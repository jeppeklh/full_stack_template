using Domain.DTO;
using Domain.Entities;
using Infrastructure.Mapping;

namespace Tests.Infrastructure.Mapping
{
    public class UserMappingTests
    {
        [Fact]
        public void UpdateFromDto_NormalizesEmailAndUsername()
        {
            // Arrange
            var entity = new User();

            var dto = new UserDTO
            {
                Initials = "AAA",
                FullName = "Test User",
                Email = "test@mail.com",
                DepartmentId = null,
                DoctorTypeId = null,
                UserStatus = Domain.Enums.UserStatus.Active
            };

            entity.UpdateFromDto(dto);

            // Assert
            Assert.Equal("test@mail.com", entity.Email);
            Assert.Equal("test@mail.com", entity.UserName);
            Assert.Equal("TEST@MAIL.COM", entity.NormalizedEmail);
            Assert.Equal("TEST@MAIL.COM", entity.NormalizedUserName);
        }
    }
}