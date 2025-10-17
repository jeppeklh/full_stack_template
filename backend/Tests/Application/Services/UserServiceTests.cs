using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain.DTO;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Moq;

namespace Tests.Application.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _repo = new();
        private readonly UserService _service;

        public UserServiceTests()
        {
            _service = new UserService(_repo.Object);
        }

        private static UserDTO BuildDto(string email = "Test@mail.com") => new()
        {
            Initials = "AAA",
            FullName = "Test User",
            Email = email,
            DepartmentId = null,
            DoctorTypeId = null,
            UserStatus = UserStatus.Active
        };

        [Fact]
        public async Task CreateUserAsync_Throws_WhenDtoIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateUserAsync(null!));
        }

        [Fact]
        public async Task CreateUserAsync_Throws_WhenEmailExists()
        {
            _repo.Setup(r => r.ExistsByEmailAsync("Test@mail.com", null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateUserAsync(BuildDto()));
            Assert.Contains("Already exists", ex.Message, StringComparison.OrdinalIgnoreCase);

            // verify that AddUserAsync was never called
            _repo.Verify(r => r.AddUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task CreateUserAsync_NormalizesEmailAndUserName()
        {
            User? savedUser = null;

            _repo.Setup(r => r.AddUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User user, CancellationToken _) =>
                {
                    savedUser = user; // capture the user being saved
                    return user;
                });

            await _service.CreateUserAsync(BuildDto("TeSt@MaIl.CoM"));

            Assert.NotNull(savedUser);
            Assert.Equal("TeSt@MaIl.CoM", savedUser.Email);
            Assert.Equal("TeSt@MaIl.CoM", savedUser.UserName);
            Assert.Equal("TEST@MAIL.COM", savedUser.NormalizedEmail);
            Assert.Equal("TEST@MAIL.COM", savedUser.NormalizedUserName);
            Assert.True(savedUser.EmailConfirmed);

            // verify that AddUserAsync was called once
            _repo.Verify(r => r.AddUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeactivateUserAsync_SetsStatusToInactive()
        {
            var id = Guid.NewGuid();
            var existingUser = new User
            {
                Id = id,
                UserStatus = UserStatus.Active
            };

            // setup the repository to return the existing user
            _repo.Setup(r => r.GetUserByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingUser);
            
            await _service.DeactivateUserAsync(id);

            Assert.Equal(UserStatus.Inactive, existingUser.UserStatus);
            _repo.Verify(r => r.UpdateUserAsync(existingUser, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}