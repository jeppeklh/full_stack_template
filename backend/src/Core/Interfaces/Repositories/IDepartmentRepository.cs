using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<List<DoctorType>> GetDoctorTypesByDepartmentAsync(Guid departmentId, CancellationToken ct = default);
        Task AddDoctorTypeAsync(DoctorType doctorType, CancellationToken ct = default);
        Task<Department> GetDepartmentAsync(Guid departmentId, CancellationToken ct = default);
        Task<bool> DeleteDoctorTypeAsync(Guid doctorTypeId, CancellationToken ct = default);

        Task<List<Department>> GetAllDepartmentsAsync(CancellationToken ct = default);
        Task AddDepartmentAsync(Department department, CancellationToken ct = default);
        Task UpdateDepartmentAsync(Department department, CancellationToken ct = default);
        Task<bool> DeleteDepartmentAsync(Guid departmentId, CancellationToken ct = default);
        Task<DepartmentType?> GetDepartmentTypeByIdAsync(Guid departmentTypeId, CancellationToken ct = default);
    }
}
