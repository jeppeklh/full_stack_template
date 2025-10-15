using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user, CancellationToken ct = default);
        Task<User?> GetUserByIdAsync(Guid userId, CancellationToken ct = default);
        Task<IReadOnlyList<User>> GetUsersAsync(bool onlyActive, CancellationToken ct = default);
        Task<User> UpdateUserAsync(User user, CancellationToken ct = default);
        Task<bool> DeleteUserAsync(Guid userId, CancellationToken ct = default);
        Task<bool> ExistsByEmailAsync(string email, Guid? userId = null, CancellationToken ct = default);
        Task<User?> GetAdminInfoAsync(CancellationToken ct = default);
    }
}
