using Aurex_Core.ApiHelper;
using Aurex_Core.DTO.EmployeeDtos;
using Aurex_Services.ApiHelper;

namespace Aurex_Core.Interfaces.ModleInterFaces
{
    public interface IEmployeeServices
    {
        Task<ApiResponse<PagedResult<EmployeeResponseDto>>> GetAllEmployeesAsync(int page, int pageSize);
        Task<ApiResponse<EmployeeResponseDto>> GetEmployeeByIdAsync(int id);
        Task<ApiResponse<EmployeeResponseDto>> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
        Task<ApiResponse<EmployeeResponseDto>> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto);
        Task<ApiResponse<bool>> DeleteEmployeeAsync(int id);
        Task<ApiResponse<IEnumerable<EmployeeResponseDto>>> GetEmployeesByDepartmentIdAsync(int departmentId);
        Task<ApiResponse<int>> GetTotalEmployeesCountAsync();
        Task<ApiResponse<int>> GetEmployeesCountByDepartmentIdAsync(int departmentId);
        Task<ApiResponse<PagedResult<EmployeeResponseDto>>> SearchEmployeesAsync(string name, int page, int pageSize);
        Task<ApiResponse<IEnumerable<EmployeeResponseDto>>> GetTopNEmployeesByScoreAsync(int n);
    }
}
