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
        Task<IReadOnlyList<EmploymentPeriod>> GetEmploymentPeriodByUserIdAsync(Guid userId, CancellationToken ct = default);
        Task<EmploymentPeriod?> GetEmploymentPeriodByIdAsync(Guid employmentPeriodId, CancellationToken ct = default);
        Task AddEmploymentPeriodAsync(EmploymentPeriod employmentPeriod, CancellationToken ct = default);
        Task UpdateEmploymentPeriodAsync(EmploymentPeriod employmentPeriod, CancellationToken ct = default);
        Task DeleteEmploymentPeriodAsync(Guid employmentPeriodId, CancellationToken ct = default);

        // Task<IEnumerable<User>> GetUsersWithEmploymentAsync();
    }
}
