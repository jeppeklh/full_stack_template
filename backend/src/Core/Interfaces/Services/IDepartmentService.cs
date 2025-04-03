using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync();
        Task<DepartmentDTO?> GetDepartmentByIdAsync(Guid departmentId);
        Task AddDepartmentAsync(DepartmentDTO department);
        Task UpdateDepartmentAsync(DepartmentDTO department);
        Task DeleteDepartmentAsync(Guid departmentId);
        Task<DepartmentType> GetDepartmentTypeByDeparmentId(Guid departmentId);
        Task<List<DoctorType>> GetDoctorTypesByDeparmentId(Guid departmentId);

    }
}
