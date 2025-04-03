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
        Task<User> AddUserAsync(User user);
        Task<List<object>> GetUsersAsync(); // make typed
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<User> GetAdminInfoAsync();
    }
}
