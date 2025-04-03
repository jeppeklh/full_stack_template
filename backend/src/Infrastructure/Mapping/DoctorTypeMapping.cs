using Domain.DTO;
using Domain.Entities;


namespace Infrastructure.Mapping
{
    public static class DoctorTypeMapping
    {
        public static DoctorType ToEntity(this DoctorTypeDTO dto)
        {
            return new DoctorType
            {
                Name = dto.Name,
                Abbreviation = dto.Abbreviation,
                DepartmentId = dto.DepartmentId
            };
        }

        public static DoctorTypeDTO ToDTO(this DoctorType entity)
        {
            return new DoctorTypeDTO
            {
                Name = entity.Name,
                Abbreviation = entity.Abbreviation,
                DepartmentId = entity.DepartmentId
            };
        }
    }
}
