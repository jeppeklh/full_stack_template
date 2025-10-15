using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Mapping;

namespace Application.Services
{
    internal class UserEmploymentService : IUserEmploymentService
    {
        private readonly IUserEmploymentRepository _userEmploymentRepository;
        private readonly IUserRepository _userRepository;

        public UserEmploymentService(IUserEmploymentRepository userEmploymentRepository, IUserRepository userRepository)
        {
            _userEmploymentRepository = userEmploymentRepository;
            _userRepository = userRepository;
        }

        public async Task<Guid> CreateAsync(Guid userId, EmploymentPeriodDTO dto, CancellationToken ct = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.", nameof(userId));

            _ = await _userRepository.GetUserByIdAsync(userId, ct)
                ?? throw new KeyNotFoundException($"User with ID {userId} not found.");

            var employmentPeriod = dto.ToEntity();
            employmentPeriod.Id = Guid.NewGuid();
            employmentPeriod.UserId = userId; // Associate with user

            await _userEmploymentRepository.AddEmploymentPeriodAsync(employmentPeriod, ct);
            return employmentPeriod.Id;
        }

        public async Task DeleteAsync(Guid employmentPeriodId, CancellationToken ct = default)
        {
            if (employmentPeriodId == Guid.Empty)
                throw new ArgumentException("Employment Period ID cannot be empty.", nameof(employmentPeriodId));

            var existing = _userEmploymentRepository.GetEmploymentPeriodByIdAsync(employmentPeriodId, ct)
                ?? throw new KeyNotFoundException($"Employment Period with ID {employmentPeriodId} not found.");

            await _userEmploymentRepository.DeleteEmploymentPeriodAsync(employmentPeriodId, ct);
        }

        public async Task<IEnumerable<EmploymentPeriodDTO>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.", nameof(userId));

            // Check if user exists
            _ = await _userRepository.GetUserByIdAsync(userId, ct)
                ?? throw new KeyNotFoundException($"User with ID {userId} not found.");

            var periods = await _userEmploymentRepository.GetEmploymentPeriodByUserIdAsync(userId, ct);

            return periods.Select(p => p.ToDTO());
        }

        public async Task UpdateAsync(Guid employmentPeriodId, EmploymentPeriodDTO dto, CancellationToken ct = default)
        {
            if (employmentPeriodId == Guid.Empty)
                throw new ArgumentException("Employment Period ID cannot be empty.", nameof(employmentPeriodId));

            var existing = await _userEmploymentRepository.GetEmploymentPeriodByIdAsync(employmentPeriodId, ct)
                ?? throw new KeyNotFoundException($"Employment Period with ID {employmentPeriodId} not found.");

            // todto would make a new object, we need to update the exsisting one with only dates
            existing.StartDate = dto.StartDate;
            existing.EndDate = dto.EndDate;

            await _userEmploymentRepository.UpdateEmploymentPeriodAsync(existing, ct);
        }
    }
}
