using Aurex_Core.DTO.AccountDtos;
using Aurex_Infrastructure.DTO.AccountDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.Interfaces.ModleInterFaces
{
    public interface IAccountServices 
    {
        Task<String> Login(LoginDto loginDto );
        Task<UserDto> Register(RegisterDto registerDto);
        Task<string> logout(string email);
    }
}
