using Domain.DTO;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;
        private readonly ILogger<DepartmentController> _logger;


        public DepartmentController(IDepartmentService service, ILogger<DepartmentController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{departmentId:guid}")]
        public async Task<IActionResult> GetDepartment(Guid departmentId, CancellationToken ct)
            => Ok(await _service.GetDepartmentByIdAsync(departmentId , ct));


        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
            => Ok(await _service.GetAllDepartmentsAsync(ct));

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentDTO entity, CancellationToken ct)
        {
            await _service.AddDepartmentAsync(entity, ct);
            return Accepted();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] DepartmentDTO entity, CancellationToken ct)
        {
            await _service.UpdateDepartmentAsync(id, entity, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDepartment(Guid id, CancellationToken ct)
        {
            await _service.DeleteDepartmentAsync(id, ct);
            return NoContent();
        }
    }
}
