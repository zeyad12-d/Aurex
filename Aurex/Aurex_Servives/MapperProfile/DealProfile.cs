using Aurex_Core.DTO.DealDtos;
using Aurex_Core.Entites;
using AutoMapper;

namespace Aurex_Services.MapperProfile
{
    public sealed class DealProfile:Profile
    {   
        public DealProfile()
        {
            // Mapping configurations will go here
            CreateMap<Deal, DealResponseDto>()
                 .ForMember(dest => dest.ClientName, op => op.MapFrom(src => src.Client != null ? src.Client.Name : string.Empty))
                 .ForMember(dest => dest.EmployeeName, op => op.MapFrom(src => src.Employee != null ? src.Employee.Name : string.Empty))
                 .ForPath(dest => dest.ProjectTeamLeaderName, op => op.MapFrom(src => src.Project != null && src.Project.TeamLeader != null
                 ? src.Project.TeamLeader.Name
                 : string.Empty
                 ));

            CreateMap<createDealDto, Deal>()
                .ForMember(dest => dest.Amount, op => op.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Currency, op => op.MapFrom(src => src.Currency))
                .ForMember(dest => dest.Endtime, op => op.MapFrom(src => src.Endtime))
                .ForMember(dest => dest.Status, op => op.MapFrom(src => src.Status))
                .ForMember(dest => dest.ClientId, op => op.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.EmployeeId, op => op.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.ProjectId , op => op.MapFrom(src => src.ProjectId));


            // For update, only map non-null properties
            CreateMap<UpdateDealDto, Deal>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
