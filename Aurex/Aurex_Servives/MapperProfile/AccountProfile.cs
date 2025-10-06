using Aurex_Core.DTO.AccountDtos;
using Aurex_Core.Entites;
using Aurex_Infrastructure.DTO.AccountDtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Services.MapperProfile
{
    public sealed class AccountProfile:Profile
    {
        public AccountProfile()
        {
            CreateMap<User,UserDto>().ReverseMap();

            CreateMap<RegisterDto, User>().ReverseMap();

            CreateMap<LoginDto, User>().ReverseMap();
        }
    }
}
