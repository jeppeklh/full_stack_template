using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly VagtplanDbContext _db;
        public DepartmentRepository(VagtplanDbContext db) => _db = db;

        public Task AddDoctorTypeAsync(DoctorType doctorType)
        {
           await _db.DoctorTypes.AddAsync(doctorType); 
           await _db.SaveChangesAsync();
        }

        public Task<bool> DeleteDoctorTypeAsync(Guid doctorTypeId)
        {
            var doctorType = _db.DoctorTypes.FirstOrDefaultAsync(dt => dt.Id == doctorTypeId);
            if (doctorType == null) return false;

            _db.DoctorTypes.Remove(doctorType);
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<Department> GetDepartmentAsync(Guid departmentId)
        {
            var department = _db.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == departmentId);
            
            if (department == null) throw new Exception($"Department {departmentId} not found");
            return department;
        }

        public Task<List<DoctorType>> GetDoctorTypesByDepartmentAsync(Guid departmentId)
        {
            return await _db.DoctorTypes
                .AsNoTracking()
                .Where(dt => dt.DepartmentId == departmentId)
                .ToListAsync();
        }
    }
}
