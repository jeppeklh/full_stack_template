using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

using Microsoft.Extensions.Logging;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Mapping;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) => _userService = userService;

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetById(Guid userId, CancellationToken ct = default)
        {
            var user = await _userService.GetByIdAsync(userId, ct);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            var users = await _userService.GetUsersAsync(true, ct);
            return Ok(users);
        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetAdminInfo(CancellationToken ct = default)
        {
            var admin = await _userService.GetAdminAsync(ct);
            return admin is null ? NotFound() : Ok(admin);
        }

        [HttpGet("GetUsersWithEmployment")]
        public async Task<IActionResult> GetUsersWithEmployment(CancellationToken ct = default)
        {
            // try
            // {
            //     var users = await _context.Users
            //         .AsNoTracking()
            //         .Where(u => u.DoctorType != null && u.EmploymentPeriods.Any())
            //         .ToListAsync();

            //     if (!users.Any())
            //     {
            //         _logger.LogWarning("No users with employment found");
            //         return NotFound(new { message = "No users in employment" });
            //     }

            //     return Ok(users.Select(u => u.ToDTO()));
            // }
            // catch (DbException dbEx)
            // {
            //     _logger.LogError(dbEx, "Database error occurred while retrieving users with employment");
            //     return StatusCode(500, new { message = "A database error occurred" });
            // }
            // catch (Exception ex)
            // {
            //     _logger.LogError(ex, "Unexpected error occurred while retrieving users with employment");
            //     return StatusCode(500, new { message = "An unexpected error occurred" });
            // }
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDTO dto, CancellationToken ct = default)
        {
            var id = await _userService.CreateUserAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { userId = id }, null); // Return 201, null body
        }

        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> Update(Guid userId, [FromBody] UserDTO dto, CancellationToken ct = default)
        {
            await _userService.UpdateUserAsync(userId, dto, ct);
            var updatedUser = await _userService.GetByIdAsync(userId, ct);
            return Ok(updatedUser);
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> Deactivate(Guid userId, CancellationToken ct)
        {
            await _userService.DeactivateUserAsync(userId, ct);
            return NoContent();
        }

        // [HttpDelete("Delete/{id}")]
        // public async Task<IActionResult> Delete(Guid id)
        // {
        //     await _userService.DeleteUserAsync(id);
        //     return NoContent();
        // }
    }
}