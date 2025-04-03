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
    public class PermissionController : ControllerBase
    {
        private readonly VagtplanDbContext _context;
        private readonly ILogger<PermissionController> _logger;

        public PermissionController(VagtplanDbContext context, ILogger<PermissionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{permissionId}")]
        public async Task<IActionResult> GetById(Guid permissionId)
        {
            try
            {

                var permission = await _context.Permissions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == permissionId);

                if (permission == null)
                {
                    _logger.LogWarning("Permission {permissionId} not found", permissionId);
                    return NotFound(new { message = "Permission not found" });
                }

                return Ok(permission.ToDTO());
            }
            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while retrieving permission {permissionId}", permissionId);
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving permission {permissionId}", permissionId);
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var permissions = await _context.Permissions
                    .AsNoTracking()
                    .ToListAsync();

                if (!permissions.Any())
                {
                    _logger.LogWarning("No permissions found");
                    return NotFound(new { message = "No permissions found" });
                }

                return Ok(permissions);
            }
            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while retrieving permissions");
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving permissions");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        

        [HttpPost("Add")]
        public async Task<IActionResult> Create([FromBody] PermissionDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.LogWarning("DTO was null");
                    return BadRequest(new { message = "DTO wasn't provided" });
                }

                if (string.IsNullOrEmpty(dto.Name))
                {
                    _logger.LogWarning("Permission name is required");
                    return BadRequest(new { message = "Permission name is required" });
                }

                var permission = dto.ToEntity();

                await _context.Permissions.AddAsync(permission);
                await _context.SaveChangesAsync();

                return Ok(permission.ToDTO());
            }
            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while creating permission");
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating permission");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Permission entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LogWarning("DTO was null");
                    return BadRequest(new { message = "DTO wasn't provided" });
                }

                var existingPermission = await _context.Permissions
                    .FirstOrDefaultAsync(p => p.Id == entity.Id);

                if (existingPermission == null)
                {
                    _logger.LogWarning("Permission {permissionId} not found", entity.Id);
                    return NotFound(new { message = "permissionId not found" });
                }

                existingPermission.Read = entity.Read;
                existingPermission.Write = entity.Write;
                existingPermission.Edit = entity.Edit;
                existingPermission.Delete = entity.Delete;

                await _context.SaveChangesAsync();

                return Ok(existingPermission.ToDTO());
            }
            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while updating permission {PermissionName}", entity.Name);
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while updating permission {PermissionName}", entity.Name);
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        [HttpDelete("Delete/{permissionId}")]
        public async Task<IActionResult> Delete(Guid permissionId)
        {
            try
            {
                var permission = await _context.Permissions
                    .FirstOrDefaultAsync(p => p.Id == permissionId);

                if (permission == null)
                {
                    _logger.LogWarning("Permission {permissionId} not found", permissionId);
                    return NotFound(new { message = "Permission not found" });
                }

                _context.Permissions.Remove(permission);
                await _context.SaveChangesAsync();

                return Ok(permission.ToDTO());
            }
            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while deleting permission {permissionId}", permissionId);
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while deleting permission {permissionId}", permissionId);
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }
    }
}
