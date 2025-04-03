using Domain.DTO;
using Domain.Entities;
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
        private readonly VagtplanDbContext _context;
        private readonly ILogger<EmploymentPeriodController> _logger;

        public EmploymentPeriodController(VagtplanDbContext context, ILogger<EmploymentPeriodController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{employmentPeriodId}")]
        public async Task<IActionResult> GetById(Guid employmentPeriodId)
        {
            try
            {
                var employmentPeriod = await _context.EmploymentPeriods
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == employmentPeriodId);

                if (employmentPeriod == null)
                {
                    _logger.LogWarning("Employment period {EmploymentPeriodId} not found", employmentPeriodId);
                    return NotFound(new { message = "Employment period not found" });
                }

                return Ok(employmentPeriod.ToDTO());
            }
            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while retrieving employment period {EmploymentPeriodId}", employmentPeriodId);
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving employment period {EmploymentPeriodId}", employmentPeriodId);
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var employmentPeriods = await _context.EmploymentPeriods
                    .AsNoTracking()
                    .ToListAsync();

                if (!employmentPeriods.Any())
                {
                    _logger.LogWarning("No employment periods found");
                    return NotFound(new { message = "No employment periods found" });
                }

                return Ok(employmentPeriods);
            }
            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while retrieving employment periods");
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving employment periods");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }


        [HttpGet("GetForUser/{userId}")]
        public async Task<IActionResult> GetForUser(Guid userId)
        {
            try
            {
                var employmentPeriods = await _context.Users
                    .Where(u => u.Id == userId)
                    .Select(u => u.EmploymentPeriods)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (employmentPeriods == null)
                {
                    _logger.LogWarning("No employment periods found for user {UserId}", userId);
                    return NotFound(new { message = "No employment periods found for this user" });
                }

                return Ok(employmentPeriods.Select(e => e.ToDTO()));
            }
            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while retrieving employment periods for user {UserId}", userId);
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving employment periods for user {UserId}", userId);
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Create([FromBody] EmploymentPeriodDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.LogWarning("DTO was null");
                    return BadRequest(new { message = "DTO wasn't provided" });
                }

                if (dto.StartDate == default)
                {
                    _logger.LogWarning("StartDate is required");
                    return BadRequest(new { message = "StartDate is required" });
                }

                var employmentPeriod = dto.ToEntity();

                await _context.EmploymentPeriods.AddAsync(employmentPeriod);
                await _context.SaveChangesAsync();

                return Ok(employmentPeriod.ToDTO());
            }
            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while creating employment period");
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating employment period");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] EmploymentPeriod entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LogWarning("DTO was null");
                    return BadRequest(new { message = "DTO wasn't provided" });
                }

                var existingEmploymentPeriod = await _context.EmploymentPeriods
                    .FirstOrDefaultAsync(e => e.Id == entity.Id);

                if (existingEmploymentPeriod == null)
                {
                    _logger.LogWarning("Employment period {EmploymentPeriodId} not found", entity.Id);
                    return NotFound(new { message = "Employment period not found" });
                }

                existingEmploymentPeriod.StartDate = entity.StartDate;
                existingEmploymentPeriod.EndDate = entity.EndDate;
                existingEmploymentPeriod.UserId = entity.UserId;

                await _context.SaveChangesAsync();

                return Ok(existingEmploymentPeriod.ToDTO());
            }
            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while updating employment period {EmploymentPeriodId}", entity.Id);
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while updating employment period {EmploymentPeriodId}", entity.Id);
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id == default)
                {
                    _logger.LogWarning("Id was null");
                    return BadRequest(new { message = "Id wasn't provided" });
                }

                var employmentPeriod = await _context.EmploymentPeriods
                    .FindAsync(id);

                if (employmentPeriod == null)
                {
                    _logger.LogWarning("Employment period {EmploymentPeriodId} not found", id);
                    return NotFound(new { message = "Employment period not found" });
                }

                _context.EmploymentPeriods.Remove(employmentPeriod);
                await _context.SaveChangesAsync();

                return Ok(employmentPeriod.ToDTO());
            }
            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while deleting employment period {EmploymentPeriodId}", id);
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while deleting employment period {EmploymentPeriodId}", id);
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }
    }
}
