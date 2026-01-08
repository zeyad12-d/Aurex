using Aurex_Core.ApiHelper;
using Aurex_Core.DTO.EmployeeDtos;
using Aurex_Core.Entites;
using Aurex_Core.Interfaces;
using Aurex_Core.Interfaces.ModelInterfaces;

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
        #region Employee CRUD Operations

        #region Get All Employees with Pagination
        public async Task<ApiResponse<PagedResult<EmployeeResponseDto>>> GetAllEmployeesAsync(int page, int pageSize)
        {
            try
            {
                page = page <= 0 ? 1 : page;
                pageSize = pageSize <= 0 ? 12 : pageSize;

                var repo = _unitOfWork.Repository<Employee>();
                var query = repo.GetQueryable()
                    .Include(e => e.Department);

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

        #region Get Employee By Id
        public async Task<ApiResponse<EmployeeResponseDto>> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var repo = _unitOfWork.Repository<Employee>();
                var employee = await repo.GetQueryable()
                    .Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (employee == null)
                {
                    return ApiResponse<EmployeeResponseDto>.CreateFail("Employee not found.");
                }
                var employeeDto = _mapper.Map<EmployeeResponseDto>(employee);
                return ApiResponse<EmployeeResponseDto>.CreateSuccess(employeeDto, "Employee retrieved successfully.");

            }
            catch (Exception ex)
            {
                return ApiResponse<EmployeeResponseDto>.CreateFail($"Error retrieving employee: {ex.Message}");
            }
        }
        #endregion

        #region create Employee
        public async Task<ApiResponse<EmployeeResponseDto>> CreateEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return ApiResponse<EmployeeResponseDto>.CreateFail("Employee data is required.");
            }

            try
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                var repo = _unitOfWork.Repository<Employee>();
                await repo.AddAsync(employee);
                await _unitOfWork.CompleteAsync();

                // Retrieve the created employee with Department included
                var createdEmployee = await repo.GetQueryable()
                    .Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.Id == employee.Id);

                var employeeResponseDto = _mapper.Map<EmployeeResponseDto>(createdEmployee);
                return ApiResponse<EmployeeResponseDto>.CreateSuccess(employeeResponseDto, "Employee created successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse<EmployeeResponseDto>.CreateFail($"Error creating employee: {ex.Message}");
            }
        }
        #endregion

        #region Update Employee
        public async Task<ApiResponse<EmployeeResponseDto>> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployee)
        {
            if (updateEmployee == null || updateEmployee.Id <= 0)
            {
                return ApiResponse<EmployeeResponseDto>.CreateFail("Valid employee data is required.");
            }
            try
            {
                var repo = _unitOfWork.Repository<Employee>();
                var existingEmployee = await repo.GetQueryable()
                    .Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.Id == updateEmployee.Id);
                if (existingEmployee == null)
                {
                    return ApiResponse<EmployeeResponseDto>.CreateFail("Employee not found.");
                }
                _mapper.Map(updateEmployee, existingEmployee);
                repo.Update(existingEmployee);
                await _unitOfWork.CompleteAsync();
                
                // Retrieve the updated employee with Department included
                var updatedEmployee = await repo.GetQueryable()
                    .Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.Id == updateEmployee.Id);
                    
                var employeeResponseDto = _mapper.Map<EmployeeResponseDto>(updatedEmployee);
                return ApiResponse<EmployeeResponseDto>.CreateSuccess(employeeResponseDto, "Employee updated successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse<EmployeeResponseDto>.CreateFail($"Error updating employee: {ex.Message}");
            }
        }
        #endregion

        #region Delete Employee
        public async Task<ApiResponse<bool>> DeleteEmployeeAsync(int id)
        {
            if (id <= 0)
            {
                return ApiResponse<bool>.CreateFail("Valid employee ID is required.");
            }
            try
            {
                var repo = _unitOfWork.Repository<Employee>();
                var ExistingEmployee = await repo.GetByIdAsync(id);
                if (ExistingEmployee == null)
                {
                    return ApiResponse<bool>.CreateFail("Employee not found.");
                }
                repo.Delete(ExistingEmployee);
                await _unitOfWork.CompleteAsync();
                return ApiResponse<bool>.CreateSuccess(true, "Employee deleted successfully.");

            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.CreateFail($"Error deleting employee: {ex.Message}");
            }
        }
        #endregion
        #endregion

        #region Additional Methods for Employee Statistics and Search

        #region Get Employees By Department Id
        public async Task<ApiResponse<IEnumerable<EmployeeResponseDto>>> GetEmployeesByDepartmentIdAsync(int departmentId)
        {
            if (departmentId <= 0)
            {
                return ApiResponse<IEnumerable<EmployeeResponseDto>>.CreateFail("Valid department ID is required.");
            }

            try
            {
                var repo = _unitOfWork.Repository<Employee>();
                var query = repo.GetQueryable()
                    .Include(e => e.Department);
                var employees = await query
                    .Where(e => e.DepartmentId == departmentId)
                    .ToListAsync();

                if (employees == null || employees.Count == 0)
                {
                    return ApiResponse<IEnumerable<EmployeeResponseDto>>.CreateFail("No employees found for the specified department.");
                }

                var employeeDtos = _mapper.Map<IEnumerable<EmployeeResponseDto>>(employees);
                return ApiResponse<IEnumerable<EmployeeResponseDto>>.CreateSuccess(employeeDtos, "Employees retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<EmployeeResponseDto>>.CreateFail($"Error retrieving employees by department: {ex.Message}");
            }
        }
        #endregion

        #region Get Total Employees Count
        public async Task<ApiResponse<int>> GetTotalEmployeesCountAsync()
        {
            try
            {
                var repo = _unitOfWork.Repository<Employee>();
                var query = repo.GetQueryable();
                var totalCount = await query.CountAsync();
                return ApiResponse<int>.CreateSuccess(totalCount, "Total employees count retrieved successfully.");
            }
            catch (Exception Ex)
            {
                return ApiResponse<int>.CreateFail($"Error retrieving total employees count: {Ex.Message}");
            }

        }

        #endregion

        #region Get Employees Count By Department Id
        public async Task<ApiResponse<int>> GetEmployeesCountByDepartmentIdAsync(int DepartmentId)
        {
            if (DepartmentId <= 0)
            {
                return ApiResponse<int>.CreateFail("Valid department ID is required.");
            }
            try
            {
                var repo = _unitOfWork.Repository<Employee>();
                var query = repo.GetQueryable();
                var count = await query.CountAsync(e => e.DepartmentId == DepartmentId);
                return ApiResponse<int>.CreateSuccess(count, "Employees count by department retrieved successfully.");

            }
            catch (Exception ex)
            {

                return ApiResponse<int>.CreateFail($"Error retrieving employees count by department: {ex.Message}");
            }
        }
        #endregion

        #region Search Employees
        public async Task<ApiResponse<PagedResult<EmployeeResponseDto>>> SearchEmployeesAsync(string name, int page, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ApiResponse<PagedResult<EmployeeResponseDto>>.CreateFail("Search term is required.");

            try
            {
                page = page <= 0 ? 1 : page;
                pageSize = pageSize <= 0 ? 12 : pageSize;

                var repo = _unitOfWork.Repository<Employee>();
                var query = repo.GetQueryable()
                                .Include(e => e.Department)
                                .AsNoTracking()
                                .Where(e => e.Name.ToLower().Contains(name.ToLower()));

                var totalCount = await query.CountAsync();

                if (totalCount == 0)
                    return ApiResponse<PagedResult<EmployeeResponseDto>>.CreateFail("No employees found matching the search criteria.");

                var employees = await query
                    .OrderBy(e => e.Name)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var employeeDtos = _mapper.Map<IEnumerable<EmployeeResponseDto>>(employees);

                var pagedResult = new PagedResult<EmployeeResponseDto>(employeeDtos, page, pageSize, totalCount);

                return ApiResponse<PagedResult<EmployeeResponseDto>>.CreateSuccess(pagedResult, "Employees retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse<PagedResult<EmployeeResponseDto>>.CreateFail($"Error searching employees: {ex.Message}");
            }
        }
        #endregion

        #region Get Top N Employees By Score

        public async Task< ApiResponse<IEnumerable<EmployeeResponseDto>>> GetTopNEmployeesByScoreAsync(int n)
        {
            if (n <= 0)
            {
                return ApiResponse<IEnumerable<EmployeeResponseDto>>.CreateFail("N must be greater than zero.");
            }
            var repo = _unitOfWork.Repository<Employee>();
            var query = repo.GetQueryable()
                .Include(e => e.Department);
            var topEmployees = await query
                .OrderByDescending(e => e.Score)
                .Take(n)
                .ToListAsync();
            if (topEmployees == null || !topEmployees.Any())
            {
                return ApiResponse<IEnumerable<EmployeeResponseDto>>.CreateFail("No employees found.");
            }
                var topEmployeeDtos = _mapper.Map<IEnumerable<EmployeeResponseDto>>(topEmployees);
            return ApiResponse<IEnumerable<EmployeeResponseDto>>.CreateSuccess(topEmployeeDtos,"Data receved Successfully"); 

        }

       
        #endregion


        #endregion
    }
}




