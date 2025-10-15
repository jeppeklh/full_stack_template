using Domain.DTO;
using Domain.Entities;

namespace Infrastructure.Mapping
{
    public static class UserMapping
    {
        public static User ToEntity(this UserDTO dto)
        {
            return new User
            {
                Initials = dto.Initials,
                FullName = dto.FullName,
                UserStatus = dto.UserStatus,
                DepartmentId = dto.DepartmentId,
                DoctorTypeId = dto.DoctorTypeId,
                Email = dto.Email
            };
        }

        public static UserDTO ToDTO(this User entity)
        {
            return new UserDTO
            {
                Initials = entity.Initials,
                FullName = entity.FullName,
                UserStatus = entity.UserStatus,
                DepartmentId = entity.DepartmentId,
                DoctorTypeId = entity.DoctorTypeId,
                Email = entity.Email
            };
        }
    }
}
