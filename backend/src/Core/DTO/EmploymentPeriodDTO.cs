using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
        public class EmploymentPeriodDTO
        {
                [DataType(DataType.Date), DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
                public DateTime StartDate { get; set; }

                [DataType(DataType.Date), DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
                public DateTime? EndDate { get; set; }

                // i dont understand why we have this range validation on a guid
                // [Range(0, int.MaxValue, ErrorMessage = "ID number must be between 0 and int32")]
                public Guid? UserId { get; set; }
        }
}
