using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Services;
using Infrastructure.Mapping;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace WebAPI.Controllers
{
	[ApiController]
	[Route("api/departments")]
	public class DepartmentController : ControllerBase
	{
		private readonly VagtplanDbContext _context;
		private readonly ILogger<DepartmentController> _logger;


		public DepartmentController(VagtplanDbContext context, ILogger<DepartmentController> logger)
		{
			_context = context;
			_logger = logger;
		}

		[HttpGet("{departmentId}")]
		public async Task<IActionResult> GetDepartment(Guid departmentId)
		{
			try
			{
				if (departmentId == Guid.Empty)
				{
                    _logger.LogWarning("Department id was null");
                    return NotFound(new { message = "Department id not found" });
                }

                var department = await _context.Departments
					.AsNoTracking()
					.FirstOrDefaultAsync(x => x.Id == departmentId);

                if (department == null)
                {
                    _logger.LogWarning("Department {DepartmentId} not found", departmentId);
                    return NotFound(new { message = "Department not found" });
                }


                return Ok(department.ToDTO());
            }

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred retrieving department {departmentId}", departmentId);
                return StatusCode(500, new { message = "A database error occurred" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving department {departmentId}", departmentId);
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
                var departments = await _context.Departments
                .AsNoTracking()
                .ToListAsync();

                if (!departments.Any())
                {
                    _logger.LogWarning("Departments not found");
                    return NotFound(new { message = "Departments not found" });
                }

                return Ok(departments);
            }
			
			catch (DbUpdateException dbEx)
			{
				_logger.LogError(dbEx, "Database error occurred retrieving list of departments");
				return StatusCode(500, new { message = "A database error occured" });
			}
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving departments");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }

        }

		[HttpPost]
		public async Task<IActionResult> AddDepartment([FromBody] DepartmentDTO entity)
		{
			try
			{
				if (entity == null)
				{
                    _logger.LogWarning("Entity was null");
                    return BadRequest(new { message = "Entity wasnt given" });
                }

				var department = DepartmentMapping.ToEntity(entity);

				await _context.Departments.AddAsync(department);
				await _context.SaveChangesAsync();


				return Ok(department.ToDTO());
            }

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while creating department");
                return StatusCode(500, new { message = "A database error occured" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating department");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }


		}

		[HttpPut]
		public async Task<IActionResult> UpdateDepartment([FromBody] Department entity)
		{
			try
			{
				var DepartmentToUpdate = await _context.Departments.FindAsync(entity.Id);

				if (DepartmentToUpdate == null)
				{
                    _logger.LogWarning("Id on department not found");
                    return BadRequest(new { message = "No department contains the Id provided" });
                }

				//Opdaterer de 3 attributter
				DepartmentToUpdate.Name = entity.Name;
				DepartmentToUpdate.Address = entity.Address;
				DepartmentToUpdate.DepartmentTypeId = entity.DepartmentTypeId;

				await _context.SaveChangesAsync();

				return Ok(DepartmentToUpdate.ToDTO());

			}

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while editing department");
                return StatusCode(500, new { message = "A database error occured" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing department");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }

		}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            try
            {
                if (id == default)
                {
                    _logger.LogWarning("Id was null.");
                    return BadRequest(new { message = "Id wasnt provided." });
                }

                var departmentToDelete = await _context.Departments.FindAsync(id);

                if (departmentToDelete == null)
                {
                    _logger.LogWarning("Found no corresponding department");
                    return BadRequest(new { message = "No department has the Id provided." });
                }

                _context.Departments.Remove(departmentToDelete);
                await _context.SaveChangesAsync();

                return Ok(departmentToDelete.ToDTO());
            }

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while creating department");
                return StatusCode(500, new { message = "A database error occured" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating department");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }
    }
}
