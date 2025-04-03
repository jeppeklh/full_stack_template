using Domain.DTO;
using Domain.Entities;

namespace Infrastructure.Mapping
{
    public static class DepartmentMapping
    {
        public static Department ToEntity(this DepartmentDTO dto)
        {
            return new Department
            {
                Name = dto.Name,
                Address = dto.Address,
                DepartmentTypeId = dto.DepartmentTypeId
            };
        }

        public static DepartmentDTO ToDTO(this Department entity)
        {
            return new DepartmentDTO
            {
                Name = entity.Name,
                Address = entity.Address,
                DepartmentTypeId = entity.DepartmentTypeId
            };
        }
    }
}
