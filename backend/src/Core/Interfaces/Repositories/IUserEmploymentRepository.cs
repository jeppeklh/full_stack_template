using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IUserEmploymentRepository
    {
        Task<EmploymentPeriod?> GetEmploymentPeriodByUserIdAsync(Guid userId);
        Task<IEnumerable<User>> GetUsersWithEmploymentAsync();
        Task AddEmploymentPeriodAsync(EmploymentPeriod employmentPeriod, Guid userId);
    }
}
