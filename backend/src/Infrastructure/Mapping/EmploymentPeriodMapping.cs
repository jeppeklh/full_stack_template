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
                Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                UserId = dto.UserId
            };
        }

        public static EmploymentPeriodDTO ToDTO(this EmploymentPeriod entity)
        {
            return new EmploymentPeriodDTO
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                UserId = entity.UserId
            };
        }
    }
}
