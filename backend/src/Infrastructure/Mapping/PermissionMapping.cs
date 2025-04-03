using Domain.DTO;
using Domain.Entities;

namespace Infrastructure.Mapping
{
    public static class PermissionMapping
    {
        public static Permission ToEntity(this PermissionDTO dto)
        {
            return new Permission
            {
                Name = dto.Name,
                Read = dto.Read,
                Write = dto.Write,
                Edit = dto.Edit,
                Delete = dto.Delete
            };
        }

        public static PermissionDTO ToDTO(this Permission entity)
        {
            return new PermissionDTO
            {
                Name = entity.Name,
                Read = entity.Read,
                Write = entity.Write,
                Edit = entity.Edit,
                Delete = entity.Delete
            };
        }
    }
}
