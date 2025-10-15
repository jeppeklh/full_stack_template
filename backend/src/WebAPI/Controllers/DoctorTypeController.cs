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
    public class DoctorTypeController : ControllerBase
    {
        private readonly VagtplanDbContext _context;
        private readonly ILogger<DoctorTypeController> _logger;

        public DoctorTypeController(VagtplanDbContext context, ILogger<DoctorTypeController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet("{doctorTypeId}")]
        public async Task<IActionResult> GetById(Guid doctorTypeId)
        {
            try
            {
                var doctorType = await _context.DoctorTypes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d => d.Id == doctorTypeId);

                if (doctorType == null)
                {
                    _logger.LogWarning("Doctor type {DoctorTypeId} not found", doctorTypeId);
                    return NotFound(new { message = "Doctor type not found" });
                }

                return Ok(doctorType.ToDTO());
            }

            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while retrieving doctor type {DoctorTypeId}", doctorTypeId);
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving doctor type {DoctorTypeId}", doctorTypeId);
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var doctorTypes = await _context.DoctorTypes
                    .AsNoTracking()
                    .ToListAsync();
                
                if (!doctorTypes.Any())
                {
                    _logger.LogWarning("No doctor types found");
                    return NotFound(new { message = "No doctor types found" });
                }

                return Ok(doctorTypes);
            }

            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while retrieving doctor types");
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving doctor types");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Create([FromBody] DoctorTypeDTO entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LogWarning("Entity was null");
                    return BadRequest(new { message = "Entity wasnt given" });
                }

                var doctorType = entity.ToEntity();

                await _context.DoctorTypes.AddAsync(doctorType);
                await _context.SaveChangesAsync();

                return Ok(doctorType.ToDTO());
            }
            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while creating doctor type");
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating doctor type");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] DoctorType entity)
        {
            try
            {
                var doctorType = await _context.DoctorTypes
                    .FindAsync(entity.Id);


                if (doctorType == null)
                {
                    _logger.LogWarning("Doctor type {DoctorTypeId} not found", entity);
                    return NotFound(new { message = "Doctor type not found" });
                }

                doctorType.Name = entity.Name;
                doctorType.Abbreviation = entity.Abbreviation;
                doctorType.DepartmentId = entity.DepartmentId;

                await _context.SaveChangesAsync();

                return Ok(doctorType.ToDTO());
            }

            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while updating doctor type {DoctorTypeId}", entity);
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while updating doctor type {DoctorTypeId}", entity);
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
                    _logger.LogWarning("Id was null.");
                    return BadRequest(new { message = "Id wasnt provided." });
                }

                var doctorType = await _context.DoctorTypes
                    .FindAsync(id);

                if (doctorType == null)
                {
                    _logger.LogWarning("Doctor type {DoctorTypeId} not found", id);
                    return NotFound(new { message = "Doctor type not found" });
                }

                // prevent deletion if any users are associated with DoctorType
                if (_context.Users.Any(u => u.DoctorTypeId == id))
                {
                    _logger.LogWarning("Doctor type {DoctorTypeId} is in use and cannot be deleted", id);
                    return Conflict(new { message = "Doctor type is in use and cannot be deleted" });
                }

                _context.DoctorTypes.Remove(doctorType);
                await _context.SaveChangesAsync();

                return Ok(doctorType.ToDTO());
            }

            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while deleting doctor type {DoctorTypeId}", id);
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while deleting doctor type {DoctorTypeId}", id);
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }
    }
}
