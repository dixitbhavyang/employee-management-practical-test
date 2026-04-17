using Microsoft.AspNetCore.Mvc;
using PracticalExercise.DTOs;
using PracticalExercise.Interfaces;

namespace PracticalExercise.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(EmployeeDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeDTO>> Create([FromBody] CreateEmployeeRequest request)
        {
            var employee = await _employeeService.CreateEmployeeAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<EmployeeDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<EmployeeDTO>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? department = null, [FromQuery] decimal? minSalary = null)
        {
            var result = await _employeeService.GetAllEmployeesAsync(page, pageSize, department, minSalary);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDTO>> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(employee);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EmployeeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDTO>> Update(int id, [FromBody] UpdateEmployeeRequest request)
        {
            var employee = await _employeeService.UpdateEmployeeAsync(id, request);
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(List<EmployeeDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<EmployeeDTO>>> GetBySearch([FromQuery] string? name)
        {
            var result = await _employeeService.GetEmployeesBySearchAsync(name);
            return Ok(result);
        }

        [HttpPost("bulk-insert")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BulkInsert([FromBody] List<CreateEmployeeRequest> requests)
        {
            var result = await _employeeService.CreateEmployeesAsync(requests);
            return Ok(result);
        }
    }
}
