using Aurex_Core.ApiHelper;
using Aurex_Core.DTO.DepartmentDtos;
using Aurex_Core.Entites;
using Aurex_Core.Interfaces;
using Aurex_Core.Interfaces.ModelInterfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Aurex_Services.Services
    {
        public class DepartmentServices : IDepartmentService
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public DepartmentServices(IUnitOfWork unit, IMapper mapper)
            {
                _mapper = mapper;
                _unitOfWork = unit;
            }
        #region Department Services

        #region Get All Departments (Paginated)
        public async Task<ApiResponse<PagedResult<DepartmentResponseDto>>> GetAllDepartments(int pageNumber, int pageSize)
            {
                pageNumber = pageNumber <= 0 ? 1 : pageNumber;
                pageSize = pageSize <= 0 ? 10 : pageSize;

                var repo = _unitOfWork.Repository<Department>();
                var query = repo.GetQueryable().AsNoTracking();

                var totalCount = await query.CountAsync();

                if (totalCount == 0)
                    return ApiResponse<PagedResult<DepartmentResponseDto>>.CreateFail("No departments found.");

                var departments = await query
                    .OrderBy(d => d.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                var departmentDtos = _mapper.Map<IEnumerable<DepartmentResponseDto>>(departments);

                var pagedResult = new PagedResult<DepartmentResponseDto>(
                    departmentDtos,
                    pageNumber,
                    pageSize,
                    totalCount
                );

                return ApiResponse<PagedResult<DepartmentResponseDto>>.CreateSuccess(pagedResult, "Departments retrieved successfully.");
            }
        #endregion

        #region Get Department By Id
        public async Task<ApiResponse<DepartmentResponseDto>> GetDepartmentById(int departmentId)
        {
            try
            {
                var repo = _unitOfWork.Repository<Department>();
                var department = await repo.GetByIdAsync(departmentId);
                if (department == null)
                    return ApiResponse<DepartmentResponseDto>.CreateFail("Department not found.");
                var departmentDto = _mapper.Map<DepartmentResponseDto>(department);
                return ApiResponse<DepartmentResponseDto>.CreateSuccess(departmentDto, "Department retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse<DepartmentResponseDto>.CreateFail($"An error occurred: {ex.Message}");
            }
        }
        #endregion

        #region Create Department
        public async Task<ApiResponse<DepartmentResponseDto>> CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            try
            {
                if (createDepartmentDto == null || string.IsNullOrWhiteSpace(createDepartmentDto.Name))
                    return ApiResponse<DepartmentResponseDto>.CreateFail("Department name is required.");

                var repo = _unitOfWork.Repository<Department>();

                
                var existingDepartment = await repo.FindAsync(d => d.Name.ToLower() == createDepartmentDto.Name.ToLower());
                if (existingDepartment != null)
                    return ApiResponse<DepartmentResponseDto>.CreateFail($"A department with the name '{createDepartmentDto.Name}' already exists.");

                
                var department = _mapper.Map<Department>(createDepartmentDto);
                await repo.AddAsync(department);
                await _unitOfWork.CompleteAsync();

                
                var departmentDto = _mapper.Map<DepartmentResponseDto>(department);

                return ApiResponse<DepartmentResponseDto>.CreateSuccess(departmentDto, "Department created successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse<DepartmentResponseDto>.CreateFail($"An error occurred while creating the department: {ex.Message}");
            }
        }


        #endregion

        #region Update Department
        public async Task<ApiResponse<DepartmentResponseDto>> UpdateDepartment(UpdateDepartmentDto updateDepartmentDto)
        {
            try
            {
                if(updateDepartmentDto==null || string.IsNullOrWhiteSpace(updateDepartmentDto.Name))
                    return ApiResponse<DepartmentResponseDto>.CreateFail("Department name is required.");

                var repo =_unitOfWork.Repository<Department>();
                var department = await repo.GetByIdAsync(updateDepartmentDto.Id);
                if (department == null)
                    return ApiResponse<DepartmentResponseDto>.CreateFail("Department not found.");
                var ExistingDepartment = await repo.FindAsync(d=>d.Name .ToLower() == updateDepartmentDto.Name.ToLower()&& d.Id != updateDepartmentDto.Id);
                if (ExistingDepartment != null)
                    return ApiResponse<DepartmentResponseDto>.CreateFail($"A department with the name '{updateDepartmentDto.Name}' already exists.");

                department.Name = updateDepartmentDto.Name;
               
                repo.Update(department);
                await _unitOfWork.CompleteAsync();
                var departmentDto = _mapper.Map<DepartmentResponseDto>(department);

                return ApiResponse<DepartmentResponseDto>.CreateSuccess(departmentDto, "Department updated successfully."); 

            }
            catch (Exception ex)
            {
                return ApiResponse<DepartmentResponseDto>.CreateFail($"An error occurred while updating the department: {ex.Message}");
            }
        }
        #endregion

        #region Delete Department
        public async Task<ApiResponse<bool>> DeleteDepartment(int DepartmentId)
        {
            try
            {
                var repo = _unitOfWork.Repository<Department>();
                var Department =await repo.GetByIdAsync(DepartmentId);
                if (Department == null)
                    return ApiResponse<bool>.CreateFail("Department not found.");
                repo.Delete(Department);
                await _unitOfWork.CompleteAsync();
                return ApiResponse<bool>.CreateSuccess(true, "Department deleted successfully.");

            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.CreateFail($"An error occurred while deleting the department: {ex.Message}");
            }
        }

      
        #endregion

        #endregion

    }
}

