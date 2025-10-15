using Aurex_Core.DTO.EmployeeDtos;
using Aurex_Services.ApiHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.Interfaces.ModleInterFaces
{
    public interface IEmployeeServices
    {
        Task<ApiResponse<IEnumerable<EmployeeResponseDto>>> GetAllEmployeesAsync( int page,int pageNumber);
        Task<ApiResponse<EmployeeResponseDto>> GetEmployeeByIdAsync(int id);
        Task<ApiResponse<EmployeeResponseDto>> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);

        Task<ApiResponse<bool>> DeleteEmployeeAsync(int id);
        Task<ApiResponse<EmployeeResponseDto>> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto);

        Task<ApiResponse<IEnumerable<EmployeeResponseDto>>> GetEmployeesByDepartmentIdAsync(int departmentId);
        Task<ApiResponse<int>> GetTotalEmployeesCountAsync();
        Task<ApiResponse<int>> GetEmployeesCountByDepartmentIdAsync(int departmentId);
        Task<ApiResponse<IEnumerable<EmployeeResponseDto>>> SearchEmployeesAsync(string name, int page, int pageSize);
        Task<ApiResponse<IEnumerable<EmployeeResponseDto>>> GetTopNEmployeesByScoreAsync(int n);

    }
}
