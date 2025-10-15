using Aurex_Core.ApiHelper;
using Aurex_Core.DTO.EmployeeDtos;
using Aurex_Core.Entites;
using Aurex_Core.Interfaces;
using Aurex_Core.Interfaces.ModleInterFaces;
using Aurex_Services.ApiHelper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Aurex_Services.Services
{
    public sealed class EmployeeServices : IEmployeeServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        #region Get All Employees with Pagination
        public async Task<ApiResponse<PagedResult<EmployeeResponseDto>>> GetAllEmployeesAsync(int page, int pageSize)
        {
            try
            {
                page = page <= 0 ? 1 : page;
                pageSize = pageSize <= 0 ? 12 : pageSize;

                var repo = _unitOfWork.Repository<Employee>();
                var query = repo.GetQueryable();

                var totalCount = await query.CountAsync();
                if (totalCount == 0)
                {
                    return ApiResponse<PagedResult<EmployeeResponseDto>>.CreateFail("No employees found.");
                }

                var employees = await query
                    .OrderBy(e => e.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var employeeDtos = _mapper.Map<IEnumerable<EmployeeResponseDto>>(employees);

                var result = new PagedResult<EmployeeResponseDto>(
                    data: employeeDtos,
                    page: page,
                    pageSize: pageSize,
                    totalCount: totalCount
                );

                return ApiResponse<PagedResult<EmployeeResponseDto>>.CreateSuccess(result, "Employees retrieved successfully.");
            }
            catch (Exception ex)
            {

                return ApiResponse<PagedResult<EmployeeResponseDto>>.CreateFail($"Error retrieving employees: {ex.Message}");
            }
        }
        #endregion

    }
}
