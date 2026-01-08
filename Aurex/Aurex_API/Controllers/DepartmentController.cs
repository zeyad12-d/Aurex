using Aurex_Core.DTO.DepartmentDtos;
using Aurex_Core.Interfaces;
using Aurex_Core.Interfaces.ModelInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aurex_API.Controllers
{
    /// <summary>
    /// Controller for managing department-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IServicesManager _servicesManager;
   
        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentController"/> class.
        /// </summary>
        /// <param name="servicesManager">The services manager.</param>
        public DepartmentController(IServicesManager servicesManager)
        {
            _servicesManager = servicesManager;
        }

        /// <summary>
        /// Gets a paginated list of departments.
        /// </summary>
        /// <param name="pageNumber">Page number (default is 1).</param>
        /// <param name="pageSize">Page size (default is 10).</param>
        /// <returns>Paginated list of departments.</returns>
        [HttpGet("Departments")]
        public async Task<IActionResult> GetAllDepartments(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _servicesManager.DepartmentService.GetAllDepartments(pageNumber, pageSize);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Gets a department by its ID.
        /// </summary>
        /// <param name="id">Department ID.</param>
        /// <returns>Department details.</returns>
        [HttpGet("Department/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var result = await _servicesManager.DepartmentService.GetDepartmentById(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="departmentDto">Department data transfer object.</param>
        /// <returns>Created department details.</returns>
        [HttpPost("Department")]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDto departmentDto)
        {
            var result = await _servicesManager.DepartmentService.CreateDepartment(departmentDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="departmentDto">Department update data transfer object.</param>
        /// <returns>Updated department details.</returns>
        [HttpPut("Department")]
        public async Task<IActionResult> UpdateDepartmentasync([FromBody] UpdateDepartmentDto departmentDto)
        {
            var result = await _servicesManager.DepartmentService.UpdateDepartment(departmentDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Deletes a department by its ID.
        /// </summary>
        /// <param name="id">Department ID.</param>
        /// <returns>Result of the delete operation.</returns>
        [HttpDelete("Department/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await _servicesManager.DepartmentService.DeleteDepartment(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
