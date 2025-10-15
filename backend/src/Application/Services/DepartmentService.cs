using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

using Infrastructure.Mapping;

namespace Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repo;
        public DepartmentService(IDepartmentRepository repo) => _repo = repo;
        public async Task AddDepartmentAsync(DepartmentDTO department, CancellationToken ct = default)
        {
            if (department == null) throw new ArgumentException("Department is required", nameof(department));
            var entity = department.ToEntity();
            await _repo.AddDepartmentAsync(entity, ct);
        }

        public async Task DeleteDepartmentAsync(Guid departmentId, CancellationToken ct = default)
        {
            if (departmentId == Guid.Empty) throw new ArgumentNullException("Department Id is required", nameof(departmentId));
            var ok = await _repo.DeleteDepartmentAsync(departmentId, ct);
            if (!ok) throw new KeyNotFoundException($"Department {departmentId} not found");
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync(CancellationToken ct = default)
        {
            var departments = await _repo.GetAllDepartmentsAsync(ct);
            return departments.Select(d => d.ToDTO());
        }

        public async Task<DepartmentDTO?> GetDepartmentByIdAsync(Guid departmentId, CancellationToken ct = default)
        {
            if (departmentId == Guid.Empty) throw new ArgumentException("Department Id is required", nameof(departmentId));
            var department = await _repo.GetDepartmentAsync(departmentId, ct);
            return department.ToDTO();
        }

        public async Task<DepartmentType> GetDepartmentTypeByDeparmentId(Guid departmentId, CancellationToken ct = default )
        {
            if (departmentId == Guid.Empty) throw new ArgumentException("Department Id is required", nameof(departmentId));
            var departmentType = await _repo.GetDepartmentTypeByIdAsync(departmentId, ct);
            return departmentType ?? throw new KeyNotFoundException($"Department type for department {departmentId} not found");
        }

        public Task<List<DoctorType>> GetDoctorTypesByDeparmentId(Guid departmentId, CancellationToken ct = default)
        {
            if (departmentId == Guid.Empty) throw new ArgumentException("Department Id is required", nameof(departmentId));
            return _repo.GetDoctorTypesByDepartmentAsync(departmentId, ct);
        }

        public async Task UpdateDepartmentAsync(Guid id, DepartmentDTO department, CancellationToken ct = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("Department Id is required", nameof(id));
            if (department == null) throw new ArgumentException("Department is required", nameof(department));

            var existing = await _repo.GetDepartmentAsync(id, ct);
            existing.Name = department.Name;
            existing.Address = department.Address;
            existing.DepartmentTypeId = department.DepartmentTypeId;

            await _repo.UpdateDepartmentAsync(existing);
        }
    }
}
