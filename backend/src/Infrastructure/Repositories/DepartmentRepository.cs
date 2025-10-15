using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly VagtplanDbContext _db;
        public DepartmentRepository(VagtplanDbContext db) => _db = db;


        public async Task AddDoctorTypeAsync(DoctorType doctorType, CancellationToken ct = default)
        {
            await _db.DoctorTypes.AddAsync(doctorType, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteDoctorTypeAsync(Guid doctorTypeId, CancellationToken ct = default)
        {
            var doctorType = await _db.DoctorTypes.FirstOrDefaultAsync(dt => dt.Id == doctorTypeId, ct);
            if (doctorType == null) return false;

            _db.DoctorTypes.Remove(doctorType);
            await _db.SaveChangesAsync(ct);
            return true;
        }


        public async Task<Department> GetDepartmentAsync(Guid departmentId, CancellationToken ct = default)
        {
            var department = await _db.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == departmentId, ct) ?? throw new Exception($"Department {departmentId} not found");
            return department;
        }


        public async Task<List<DoctorType>> GetDoctorTypesByDepartmentAsync(Guid departmentId, CancellationToken ct = default)
        {
            return await _db.DoctorTypes
                .AsNoTracking()
                .Where(dt => dt.DepartmentId == departmentId)
                .ToListAsync(ct);
        }

        public async Task AddDepartmentAsync(Department department, CancellationToken ct = default)
        {
            await _db.Departments.AddAsync(department, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteDepartmentAsync(Guid departmentId, CancellationToken ct = default)
        {
            var department = await _db.Departments.FirstOrDefaultAsync(d => d.Id == departmentId);
            if (department == null) return false;

            _db.Departments.Remove(department);
            await _db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<List<Department>> GetAllDepartmentsAsync(CancellationToken ct = default) => await _db.Departments.AsNoTracking().ToListAsync(ct);

        public async Task<DepartmentType?> GetDepartmentTypeByIdAsync(Guid departmentTypeId, CancellationToken ct = default)
        {
            var department = await _db.Departments.AsNoTracking()
                .Select(d => new { d.Id, d.DepartmentTypeId }) // Select only id and DepartmentTypeId
                .FirstOrDefaultAsync(d => d.Id == departmentTypeId, ct);

            if (department?.DepartmentTypeId == null) return null;

            return await _db.DepartmentTypes.AsNoTracking()
                .FirstOrDefaultAsync(dt => dt.Id == department.DepartmentTypeId, ct);
        }

        public async Task UpdateDepartmentAsync(Department department, CancellationToken ct = default)
        {
            _db.Departments.Update(department);
            await _db.SaveChangesAsync(ct);
        }
    }
}
