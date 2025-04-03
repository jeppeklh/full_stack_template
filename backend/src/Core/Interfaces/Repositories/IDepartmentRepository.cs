using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<List<DoctorType>> GetDoctorTypesByDepartmentAsync(Guid departmentId);
        Task AddDoctorTypeAsync(DoctorType doctorType);
        Task<Department> GetDepartmentAsync(Guid departmentId);
        Task<bool> DeleteDoctorTypeAsync(Guid doctorTypeId);
    }
}
