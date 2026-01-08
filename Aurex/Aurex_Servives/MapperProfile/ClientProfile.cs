using Aurex_Core.DTO.ClientDtos;
using Aurex_Core.Entites;
using AutoMapper;

namespace Aurex_Services.MapperProfile
{
    public sealed class ClientProfile:Profile
    {
        public ClientProfile()
        {
            CreateMap<Client , ClientDealsDto>()
                .ForMember(dest => dest.ClientId, op => op.MapFrom(src => src.Id))
                .ForMember(dest => dest.ClientName, op => op.MapFrom(src => src.Name))
                .ForMember(dest => dest.Deals, op => op.MapFrom(src => src.Deals));

            CreateMap<Client, ClientResponseDto>()
                .ForMember(dest => dest.status, op => op.MapFrom(src => src.Status));

            CreateMap<Deal, DealResponse>()
                .ForMember(dest => dest.Id, op => op.MapFrom(src => src.Id))
                .ForMember(dest => dest.Amount, op => op.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Status, op => op.MapFrom(src => src.Status))
                .ForMember(dest => dest.Endtime, op => op.MapFrom(src => src.Endtime)); 
            
          CreateMap<CreateClientDto, Client>();

            // create partial update 
            CreateMap<UpdateClientDto, Client>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
