using Aurex_Core.ApiHelper;
using Aurex_Core.DTO.DepartmentDtos;
using Aurex_Services.ApiHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.Interfaces.ModleInterFaces
{
    public interface IDepartmentService
    {
       Task<ApiResponse<PagedResult<DepartmentResponseDto>>> GetAllDepartments(int pageNumber, int pageSize);
        Task<ApiResponse<DepartmentResponseDto>> GetDepartmentById(int departmentId);

        Task<ApiResponse<DepartmentResponseDto>> CreateDepartment(CreateDepartmentDto createDepartmentDto);

        Task<ApiResponse<DepartmentResponseDto>> UpdateDepartment(UpdateDepartmentDto updateDepartmentDto);

        Task<ApiResponse<bool>> DeleteDepartment(int departmentId);

    }
}
