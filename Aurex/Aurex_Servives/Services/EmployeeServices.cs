using Aurex_Core.DTO.EmployeeDtos;
using Aurex_Core.Entites;
using Aurex_Core.Interfaces;
using Aurex_Core.Interfaces.ModleInterFaces;
using Aurex_Services.ApiHelper;
using AutoMapper;

namespace Aurex_Services.Services
{
    public sealed class EmployeeServices :IEmployeeServices
    {
        public readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public EmployeeServices( IMapper mapper , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        #region Get All Employees with Pagination
        public async Task<ApiResponse<IEnumerable<EmployeeResponseDto>>> GetAllEmployee(int page, int pageSize)
        {
            try
            {
                if(page<=0) page = 1;
                if (pageSize <= 0) pageSize = 12;
                var employees = await _unitOfWork.Repository<Employee>().GetAllAsync();
                if (employees == null || !employees.Any())
                {
                    return ApiResponse<IEnumerable<EmployeeResponseDto>>.CreateFail("No employees found.");
                }
                var totalCount = employees.Count();
                var pagedEmployees = employees
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
               

                var employeeDtos = _mapper.Map<IEnumerable<EmployeeResponseDto>>(pagedEmployees);


                // Optionally, you can create a paged response DTO to include totalCount, page, pageSize, etc.
                // For now, just return the paged data.
                return ApiResponse<IEnumerable<EmployeeResponseDto>>.CreateSuccess(employeeDtos, $"Employees retrieved successfully. Page {page} of {Math.Ceiling((double)totalCount / pageSize)}.");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<EmployeeResponseDto>>.CreateFail($"Error retrieving employees: {ex.Message}");
            }
        }
        #endregion
    }
}
