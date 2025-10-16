using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class DepartmentDTO
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Department name has to be longer than 3 characters, and it can't exceed 100 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Department address has to be longer than 3 characters, and it can't exceed 100 characters.")]
        public string Address { get; set; }

        // i dont understand why we have this range validation on a guid
        // [Range(0, int.MaxValue, ErrorMessage ="ID number must be between 0 and int32")]
        public Guid? DepartmentTypeId { get; set; }
    }
}
