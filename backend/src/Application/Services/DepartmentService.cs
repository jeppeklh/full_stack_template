using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        public Task AddDepartmentAsync(DepartmentDTO department)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDepartmentAsync(Guid departmentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentDTO?> GetDepartmentByIdAsync(Guid departmentId)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentType> GetDepartmentTypeByDeparmentId(Guid departmentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<DoctorType>> GetDoctorTypesByDeparmentId(Guid departmentId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDepartmentAsync(DepartmentDTO department)
        {
            throw new NotImplementedException();
        }
    }
}
