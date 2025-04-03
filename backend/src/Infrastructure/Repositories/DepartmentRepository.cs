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
        public Task AddDoctorTypeAsync(DoctorType doctorType)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDoctorTypeAsync(Guid doctorTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetDepartmentAsync(Guid departmentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<DoctorType>> GetDoctorTypesByDepartmentAsync(Guid departmentId)
        {
            throw new NotImplementedException();
        }
    }
}
