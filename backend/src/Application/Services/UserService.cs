using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Mapping;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<Guid> CreateUserAsync(UserDTO userDto, CancellationToken ct = default)
        {
            if (userDto == null)
                throw new ArgumentNullException("User is required", nameof(userDto));

            if (await _userRepository.ExistsByEmailAsync(userDto.Email, null, ct))
                throw new ArgumentException($"A user with email {userDto.Email} already exists.");

            var user = new User { Id = Guid.NewGuid(), SecurityStamp = Guid.NewGuid().ToString() };
            user.UpdateFromDto(userDto);
            user.EmailConfirmed = true; 
            var createdUser = await _userRepository.AddUserAsync(user, ct);

            return createdUser.Id;
        }

        public async Task DeactivateUserAsync(Guid userId, CancellationToken ct = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID is required.", nameof(userId));

            var user = await _userRepository.GetUserByIdAsync(userId, ct)
                ?? throw new KeyNotFoundException($"User with ID {userId} not found.");

            if (user.UserStatus == UserStatus.Inactive) return; // Already inactive

            user.UserStatus = UserStatus.Inactive;
            await _userRepository.UpdateUserAsync(user, ct);
        }

        public async Task<UserDTO?> GetAdminAsync(CancellationToken ct = default)
        {
            var admin = await _userRepository.GetAdminInfoAsync(ct);
            return admin?.ToDTO();
        }

        public async Task<UserDTO> GetByIdAsync(Guid userId, CancellationToken ct = default)
        {
            if (userId == Guid.Empty) throw new ArgumentException("User ID is required.", nameof(userId));

            var user = await _userRepository.GetUserByIdAsync(userId, ct)
                ?? throw new KeyNotFoundException($"User with ID {userId} not found.");

            return user.ToDTO();
        }

        public async Task<IReadOnlyList<UserDTO>> GetUsersAsync(bool onlyActive, CancellationToken ct = default)
        {
            var users = await _userRepository.GetUsersAsync(onlyActive, ct);
            return users.Select(u => u.ToDTO()).ToList();
        }

        public async Task UpdateUserAsync(Guid userId, UserDTO userDto, CancellationToken ct = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID is required.", nameof(userId));

            if (userDto == null)
                throw new ArgumentNullException("User is required", nameof(userDto));

            var user = await _userRepository.GetUserByIdAsync(userId, ct)
                ?? throw new KeyNotFoundException($"User with ID {userId} not found.");

            if (await _userRepository.ExistsByEmailAsync(userDto.Email, userId, ct))
                throw new ArgumentException($"A user with email {userDto.Email} already exists.");

            user.UpdateFromDto(userDto);
            await _userRepository.UpdateUserAsync(user, ct);
        }
    }
}
