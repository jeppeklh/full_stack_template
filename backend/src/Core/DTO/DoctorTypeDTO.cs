using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class DoctorTypeDTO
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Doctor type name must be longer than 3 characters, and cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Doctor type abbrevation must be longer than 0 characters, and cannot exceed 10 characters.")]
        public string Abbreviation { get; set; }

        // i dont understand why we have this range validation on a guid
        // [Range(0, int.MaxValue, ErrorMessage = "ID number must be between 0 and int32")]
        public Guid? DepartmentId { get; set; }
    }
}
