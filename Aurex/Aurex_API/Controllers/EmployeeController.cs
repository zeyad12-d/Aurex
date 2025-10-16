using Aurex_Core.DTO.EmployeeDtos;
using Aurex_Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aurex_API.Controllers
{
    /// <summary>
    /// Controller for managing employee-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IServicesManager _servicesManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="servicesManager">The services manager.</param>
        public EmployeeController(IServicesManager servicesManager)
        {
            _servicesManager = servicesManager;
        }

        /// <summary>
        /// Gets a paginated list of all employees.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of employees.</returns>
        [HttpGet("Employees")]
        public async Task<IActionResult> GetAllEmployees(int page = 1, int pageSize = 12)
        {
            var result = await _servicesManager.EmployeeServices.GetAllEmployeesAsync(page, pageSize);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Gets an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The employee ID.</param>
        /// <returns>The employee details.</returns>
        [HttpGet("Employee/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var result = await _servicesManager.EmployeeServices.GetEmployeeByIdAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="createEmployeeDto">The employee data transfer object.</param>
        /// <returns>The created employee details.</returns>
        [HttpPost("Employee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            var result = await _servicesManager.EmployeeServices.CreateEmployeeAsync(createEmployeeDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="updateEmployeeDto">The employee update data transfer object.</param>
        /// <returns>The updated employee details.</returns>
        [HttpPut("Employee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            var result = await _servicesManager.EmployeeServices.UpdateEmployeeAsync(updateEmployeeDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Deletes an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The employee ID.</param>
        /// <returns>Result of the delete operation.</returns>
        [HttpDelete("Employee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _servicesManager.EmployeeServices.DeleteEmployeeAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Gets employees by department ID.
        /// </summary>
        /// <param name="departmentId">The department ID.</param>
        /// <returns>List of employees in the department.</returns>
        [HttpGet("Employees/Department/{departmentId}")]
        public async Task<IActionResult> GetEmployeesByDepartmentId(int departmentId)
        {
            var result = await _servicesManager.EmployeeServices.GetEmployeesByDepartmentIdAsync(departmentId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Gets the total count of employees.
        /// </summary>
        /// <returns>Total number of employees.</returns>
        [HttpGet("Employees/Count")]
        public async Task<IActionResult> GetTotalEmployeesCount()
        {
            var result = await _servicesManager.EmployeeServices.GetTotalEmployeesCountAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Gets the count of employees in a specific department.
        /// </summary>
        /// <param name="departmentId">The department ID.</param>
        /// <returns>Number of employees in the department.</returns>
        [HttpGet("Employees/Count/Department/{departmentId}")]
        public async Task<IActionResult> GetEmployeesCountByDepartmentId(int departmentId)
        {
            var result = await _servicesManager.EmployeeServices.GetEmployeesCountByDepartmentIdAsync(departmentId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Searches employees by name.
        /// </summary>
        /// <param name="name">The employee name to search for.</param>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of employees matching the search criteria.</returns>
        [HttpGet("Employees/Search")]
        public async Task<IActionResult> SearchEmployees(string name, int page = 1, int pageSize = 12)
        {
            var result = await _servicesManager.EmployeeServices.SearchEmployeesAsync(name, page, pageSize);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Gets the top N employees by score.
        /// </summary>
        /// <param name="n">The number of top employees to retrieve.</param>
        /// <returns>List of top N employees by score.</returns>
        [HttpGet("Employees/Top/{n}")]
        public async Task<IActionResult> GetTopNEmployeesByScore(int n)
        {
            var result = await _servicesManager.EmployeeServices.GetTopNEmployeesByScoreAsync(n);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
