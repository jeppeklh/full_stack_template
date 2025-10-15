using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;

namespace Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDTO> GetByIdAsync(Guid userId, CancellationToken ct = default);
        Task<IReadOnlyList<UserDTO>> GetUsersAsync(bool onlyActive, CancellationToken ct = default);
        Task<UserDTO?> GetAdminAsync(CancellationToken ct = default);
        Task<Guid> CreateUserAsync(UserDTO userDto, CancellationToken ct = default);
        Task UpdateUserAsync(Guid userId, UserDTO userDto, CancellationToken ct = default);
        Task DeactivateUserAsync(Guid userId, CancellationToken ct = default);

    }
}
