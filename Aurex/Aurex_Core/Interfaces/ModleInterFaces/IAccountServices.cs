using Aurex_Core.DTO.AccountDtos;
using Aurex_Services.ApiHelper; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aurex_Core.Interfaces.ModleInterFaces
{
    public interface IAccountServices
    {
        Task<ApiResponse<string>> Login(LoginDto loginDto);
        Task<ApiResponse<UserDto>> Register(RegisterDto registerDto);
        Task<ApiResponse<string>> Logout(string email);
        Task<ApiResponse<UserDto>> FindUserByEmail(string email);
        Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsers();
        Task<ApiResponse<IEnumerable<UserDto>>> GetUsersByRole(string roleName);
        Task<ApiResponse<string>> DeleteUserByEmail(string email);
    }
}
