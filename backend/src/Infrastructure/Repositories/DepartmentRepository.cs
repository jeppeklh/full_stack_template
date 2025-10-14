using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly VagtplanDbContext _db;
        public DepartmentRepository(VagtplanDbContext db) => _db = db;

        public async Task AddDoctorTypeAsync(DoctorType doctorType)
        {
           await _db.DoctorTypes.AddAsync(doctorType); 
           await _db.SaveChangesAsync();
        }

        public async Task<bool> DeleteDoctorTypeAsync(Guid doctorTypeId)
        {
            var doctorType = await _db.DoctorTypes.FirstOrDefaultAsync(dt => dt.Id == doctorTypeId);
            if (doctorType == null) return false;

            _db.DoctorTypes.Remove(doctorType);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Department> GetDepartmentAsync(Guid departmentId)
        {
            var department = await _db.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == departmentId) ?? throw new Exception($"Department {departmentId} not found");
            return department;
        }

        public async Task<List<DoctorType>> GetDoctorTypesByDepartmentAsync(Guid departmentId)
        {
            return await _db.DoctorTypes
                .AsNoTracking()
                .Where(dt => dt.DepartmentId == departmentId)
                .ToListAsync();
        }
    }
}
