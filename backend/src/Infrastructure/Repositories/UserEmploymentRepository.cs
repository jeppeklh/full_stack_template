using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserEmploymentRepository : IUserEmploymentRepository
    {
        private readonly VagtplanDbContext _db;

        public UserEmploymentRepository(VagtplanDbContext db) => _db = db;

        public async Task AddEmploymentPeriodAsync(EmploymentPeriod employmentPeriod, CancellationToken ct = default)
        {
            await _db.EmploymentPeriods.AddAsync(employmentPeriod, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteEmploymentPeriodAsync(Guid employmentPeriodId, CancellationToken ct = default)
        {
            var employmentPeriod = await _db.EmploymentPeriods.FindAsync(employmentPeriodId, ct);
            if (employmentPeriod == null) return;

            _db.EmploymentPeriods.Remove(employmentPeriod);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<EmploymentPeriod?> GetEmploymentPeriodByIdAsync(Guid employmentPeriodId, CancellationToken ct = default)
        {
            return await _db.EmploymentPeriods
                .AsNoTracking()
                .FirstOrDefaultAsync(ep => ep.Id == employmentPeriodId, ct);
        }

        public async Task<IReadOnlyList<EmploymentPeriod>> GetEmploymentPeriodByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            return await _db.EmploymentPeriods
                .Where(ep => ep.UserId == userId)
                .OrderByDescending(ep => ep.StartDate)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task UpdateEmploymentPeriodAsync(EmploymentPeriod employmentPeriod, CancellationToken ct = default)
        {
            _db.EmploymentPeriods.Update(employmentPeriod);
            await _db.SaveChangesAsync(ct);
        }
    }
}
