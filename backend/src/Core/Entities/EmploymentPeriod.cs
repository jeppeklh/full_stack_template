using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
	public class EmploymentPeriod : BaseEntity
	{
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        //FK refererer til den user som har denne employmentPeriod.
        [Range(0, int.MaxValue, ErrorMessage = "ID number must be between 0 and int32")]
        public Guid? UserId { get; set; }

		public User? User { get; set; }


		public EmploymentPeriod()
		{
		}
	}
}