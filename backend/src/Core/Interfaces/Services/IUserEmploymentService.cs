using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;

namespace Domain.Interfaces.Services
{
    public interface IUserEmploymentService
    {
        Task<IEnumerable<EmploymentPeriodDTO>> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
        Task<Guid> CreateAsync(Guid userId, EmploymentPeriodDTO dto, CancellationToken ct = default);
        Task UpdateAsync(Guid employmentPeriodId, EmploymentPeriodDTO dto, CancellationToken ct = default);
        Task DeleteAsync(Guid employmentPeriodId, CancellationToken ct = default);
    }
}
