using Domain.DTO;
using Domain.Entities;

namespace Infrastructure.Mapping
{
    public static class DepartmentTypeMapping
    {
        public static DepartmentType ToEntity(this DepartmentTypeDTO dto)
        {
            return new DepartmentType
            {
                Name = dto.Name
            };
        }

        public static DepartmentTypeDTO ToDTO(this DepartmentType entity)
        {
            return new DepartmentTypeDTO
            {
                Name = entity.Name
            };
        }
    }
}
