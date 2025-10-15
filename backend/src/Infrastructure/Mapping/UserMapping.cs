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

        public static void UpdateFromDto(this User entity, UserDTO dto)
        {
            ArgumentNullException.ThrowIfNull(entity);
            ArgumentNullException.ThrowIfNull(dto);

            entity.Initials = dto.Initials;
            entity.FullName = dto.FullName;
            entity.DepartmentId = dto.DepartmentId;
            entity.DoctorTypeId = dto.DoctorTypeId;
            entity.UserStatus = dto.UserStatus;

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var trimmed = dto.Email.Trim();
                entity.Email = trimmed;
                entity.UserName = trimmed;
                entity.NormalizedEmail = trimmed.ToUpperInvariant();
                entity.NormalizedUserName = trimmed.ToUpperInvariant();
            }
        }
    }
}
