using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Services;
using Infrastructure.Mapping;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmploymentPeriodController : ControllerBase
    {
        private readonly IUserEmploymentService _userEmploymentService;

        public EmploymentPeriodController(IUserEmploymentService userEmploymentService) => _userEmploymentService = userEmploymentService;

        // Get all (for a user) is done by user controller
        // [HttpGet("{employmentPeriodId}")]
        // public async Task<IActionResult> GetById(Guid employmentPeriodId)
        // {
        //     try
        //     {
        //         var employmentPeriod = await _context.EmploymentPeriods
        //             .AsNoTracking()
        //             .FirstOrDefaultAsync(e => e.Id == employmentPeriodId);

        //         if (employmentPeriod == null)
        //         {
        //             _logger.LogWarning("Employment period {EmploymentPeriodId} not found", employmentPeriodId);
        //             return NotFound(new { message = "Employment period not found" });
        //         }

        //         return Ok(employmentPeriod.ToDTO());
        //     }
        //     catch (DbException dbEx)
        //     {
        //         _logger.LogError(dbEx, "Database error occurred while retrieving employment period {EmploymentPeriodId}", employmentPeriodId);
        //         return StatusCode(500, new { message = "A database error occurred" });
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Unexpected error occurred while retrieving employment period {EmploymentPeriodId}", employmentPeriodId);
        //         return StatusCode(500, new { message = "An unexpected error occurred" });
        //     }
        // }

        // [HttpGet("GetAll")]
        // public async Task<IActionResult> GetAll()
        // {
        //     try
        //     {
        //         var employmentPeriods = await _context.EmploymentPeriods
        //             .AsNoTracking()
        //             .ToListAsync();

        //         if (!employmentPeriods.Any())
        //         {
        //             _logger.LogWarning("No employment periods found");
        //             return NotFound(new { message = "No employment periods found" });
        //         }

        //         return Ok(employmentPeriods);
        //     }
        //     catch (DbException dbEx)
        //     {
        //         _logger.LogError(dbEx, "Database error occurred while retrieving employment periods");
        //         return StatusCode(500, new { message = "A database error occurred" });
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Unexpected error occurred while retrieving employment periods");
        //         return StatusCode(500, new { message = "An unexpected error occurred" });
        //     }
        // }


        [HttpGet("GetForUser/{userId}")]
        public async Task<IActionResult> GetForUser(Guid userId, CancellationToken ct = default)
        {
            var periods = await _userEmploymentService.GetByUserIdAsync(userId, ct);
            return Ok(periods);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Create(Guid userId, [FromBody] EmploymentPeriodDTO dto, CancellationToken ct = default)
        {
            var id = await _userEmploymentService.CreateAsync(userId, dto, ct);
            return CreatedAtAction(nameof(GetForUser), new { userId }, new { employmentPeriodId = id }); // return the userId and employmentPeriodId in the response body
        }

        [HttpPut("{employmentPeriodId:guid}")]
        public async Task<IActionResult> Update(Guid userId, Guid employmentPeriodId, [FromBody] EmploymentPeriodDTO dto, CancellationToken ct = default)
        {
            await _userEmploymentService.UpdateAsync(employmentPeriodId, dto, ct);
            return NoContent();
        }

        [HttpDelete("{employmentPeriodId:guid}")]
        public async Task<IActionResult> Delete(Guid userId, Guid employmentPeriodId, CancellationToken ct = default)
        {
            await _userEmploymentService.DeleteAsync(employmentPeriodId, ct);
            return NoContent();
        }
    }
}
