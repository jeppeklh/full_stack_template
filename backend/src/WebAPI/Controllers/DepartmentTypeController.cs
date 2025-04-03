using Domain.DTO;
using Domain.Entities;
using Infrastructure.Mapping;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentTypeController : ControllerBase
    {
        private readonly VagtplanDbContext _context;
        private readonly ILogger<DepartmentTypeController> _logger;

        public DepartmentTypeController(VagtplanDbContext context, ILogger<DepartmentTypeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{departmentTypeId}")]
        public async Task<IActionResult> GetDepartmentType(Guid departmentTypeId)
        {
            try
            {
                if (departmentTypeId == Guid.Empty)
                {
                    _logger.LogWarning("Department id was null");
                    return NotFound(new { message = "Department id not found" });
                }


                var departmentType = await _context.DepartmentTypes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d => d.Id == departmentTypeId);

                if (departmentType == null)
                {
                    _logger.LogWarning("Department not found");
                    return NotFound(new { message = "Department not found" });
                }

                return Ok(departmentType.ToDTO());
            }

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred retrieving departmentType {departmentTypeId}", departmentTypeId);
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving department {departmentTypeId}", departmentTypeId);
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }


        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllDepartmentTypes()
        {
            var departmentTypes = await _context.DepartmentTypes
                .AsNoTracking()
                .ToListAsync();

            if (!departmentTypes.Any())
            {
                _logger.LogWarning("DepartmentTypes not found");
                return NotFound(new { message = "Department types not found" });
            }

            return Ok(departmentTypes);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddDepartmentType([FromBody] DepartmentTypeDTO entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LogWarning("Entity was null");
                    return BadRequest(new { message = "Entity wasnt given" });
                }

                var departmentType = entity.ToEntity();

                await _context.DepartmentTypes.AddAsync(departmentType);
                await _context.SaveChangesAsync();

                return Ok(departmentType.ToDTO());
            }

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while creating departmentType");
                return StatusCode(500, new { message = "A database error occured" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating department type");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }


        [HttpPut("Update")]
        public async Task<IActionResult> UpdateDepartmentType([FromBody] DepartmentType entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LogWarning("Id on department not found");
                    return BadRequest(new { message = "No department contains the Id provided" });
                }

                var departmentType = await _context.DepartmentTypes.FindAsync(entity);

                if (departmentType == null)
                {
                    _logger.LogWarning("Department type was null");
                    return BadRequest(new { message = "Department type entity wasnt found" });
                }

                departmentType.Name = entity.Name;
                await _context.SaveChangesAsync();

                return Ok(departmentType.ToDTO());
            }

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while editing department type");
                return StatusCode(500, new { message = "A database error occured" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing department type");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }

        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDepartmentType(Guid id)
        {
            try
            {
                if (id == default)
                {
                    _logger.LogWarning("Id was null.");
                    return BadRequest(new { message = "Id wasnt provided." });
                }

                var departmentType = await _context.DepartmentTypes.FindAsync(id);

                if (departmentType == null)
                {
                    _logger.LogWarning("Department type was null");
                    return BadRequest(new { message = "Department type entity wasnt found" });
                }
                    
                _context.DepartmentTypes.Remove(departmentType);
                await _context.SaveChangesAsync();

                return Ok(departmentType.ToDTO());

            }

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while deleting department");
                return StatusCode(500, new { message = "A database error occured" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting department");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }
    }
}
