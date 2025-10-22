using Aurex_Core.DTO.DepartmentDtos;
using Aurex_Core.Entites;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Services.MapperProfile
{
    public sealed class DepartmentProfile :Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department,DepartmentResponseDto>().ReverseMap();

            CreateMap<CreateDepartmentDto,Department>()
                .ForMember(x => x.Name,op=>op.MapFrom(x => x.Name));

            CreateMap<UpdateDepartmentDto, Department>()
                .ForMember(op => op.Id, o => o.MapFrom(s=>s.Id))
                .ForMember(op => op.Name,o=>o.MapFrom(s=>s.Name));
        }
    }
}
