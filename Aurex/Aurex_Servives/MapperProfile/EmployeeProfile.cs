using Aurex_Core.DTO.EmployeeDtos;
using Aurex_Core.Entites;
using AutoMapper;

namespace Aurex_Services.MapperProfile
{
    public sealed class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee,EmployeeResponseDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : "No Department"))
                .ReverseMap(); 
            CreateMap<CreateEmployeeDto, Employee>().ReverseMap();

            CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();

        }
    }
}
