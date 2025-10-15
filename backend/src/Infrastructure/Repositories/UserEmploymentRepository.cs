using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserEmploymentRepository : IUserEmploymentRepository
    {
        public Task AddEmploymentPeriodAsync(EmploymentPeriod employmentPeriod, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<EmploymentPeriod?> GetEmploymentPeriodByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersWithEmploymentAsync()
        {
            throw new NotImplementedException();
        }
    }
}
