using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Initials must be longer than 1 characters, and cannot exceed 100 characters.")]
        public string Initials { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be longer than 3 characters, and cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        public UserStatus UserStatus { get; set; }

        // i dont understand why we have this range validation on a guid
        // [Range(0, int.MaxValue, ErrorMessage = "ID number must be between 0 and int32")]
        public Guid? DepartmentId { get; set; } = Guid.Empty;
        public string? DepartmentName { get; set; }


        // i dont understand why we have this range validation on a guid
        // [Range(0, int.MaxValue, ErrorMessage = "ID number must be between 0 and int32")]
        public Guid? DoctorTypeId { get; set; } = Guid.Empty;
        public string? DoctorTypeName { get; set; } 

    }
}
