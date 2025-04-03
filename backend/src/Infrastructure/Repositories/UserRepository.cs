using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAdminInfoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<object>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
