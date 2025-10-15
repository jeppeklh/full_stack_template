using Domain.DTO;
using Domain.Entities;
namespace Domain.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync(CancellationToken ct = default);
        Task<DepartmentDTO?> GetDepartmentByIdAsync(Guid departmentId , CancellationToken ct = default);
        Task AddDepartmentAsync(DepartmentDTO department, CancellationToken ct = default);
        Task UpdateDepartmentAsync(Guid id, DepartmentDTO department , CancellationToken ct = default);
        Task DeleteDepartmentAsync(Guid departmentId, CancellationToken ct = default);
        Task<DepartmentType> GetDepartmentTypeByDeparmentId(Guid departmentId, CancellationToken ct = default);
        Task<List<DoctorType>> GetDoctorTypesByDeparmentId(Guid departmentId, CancellationToken ct = default);

    }
}
