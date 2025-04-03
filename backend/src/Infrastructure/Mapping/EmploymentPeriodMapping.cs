using Domain.DTO;
using Domain.Entities;


namespace Infrastructure.Mapping
{
    public static class EmploymentPeriodMapping
    {
        public static EmploymentPeriod ToEntity(this EmploymentPeriodDTO dto)
        {
            return new EmploymentPeriod
            {
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                UserId = dto.UserId
            };
        }

        public static EmploymentPeriodDTO ToDTO(this EmploymentPeriod entity)
        {
            return new EmploymentPeriodDTO
            {
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                UserId = entity.UserId
            };
        }
    }
}
