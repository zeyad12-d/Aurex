using Aurex_Core.DTO.AccountDtos;
using Aurex_Core.Entites;
using Aurex_Core.DTO.AccountDtos;
using Aurex_Core.Interfaces.ModleInterFaces;
using Aurex_Services.ApiHelper;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Aurex_Services.Services
{
    public sealed class AccountServices : IAccountServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountServices(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            IMapper mapper,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        #region Login
        public async Task<ApiResponse<string>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return ApiResponse<string>.CreateFail("Invalid email or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                return ApiResponse<string>.CreateFail("Invalid email or password.");

            var token = await GenerateToken(user);
            if (string.IsNullOrEmpty(token))
                return ApiResponse<string>.CreateFail("Token generation failed.");

            return ApiResponse<string>.CreateSuccess(token, "Login successful.");
        }
        #endregion

        #region Logout
        public async Task<ApiResponse<string>> Logout(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return ApiResponse<string>.CreateFail("User not found.");

            await _signInManager.SignOutAsync();
            return ApiResponse<string>.CreateSuccess(null, "User logged out successfully.");
        }
        #endregion

        #region Register
        public async Task<ApiResponse<UserDto>> Register(RegisterDto registerDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
                return ApiResponse<UserDto>.CreateFail("Email is already registered.");

            var existingName = await _userManager.FindByNameAsync(registerDto.UserName);
            if (existingName != null)
                return ApiResponse<UserDto>.CreateFail("Username is already taken.");

            var user = _mapper.Map<User>(registerDto);
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return ApiResponse<UserDto>.CreateFail($"User creation failed: {errors}");
            }

            await _userManager.AddToRoleAsync(user, "AccountPerson");

            var userDto = _mapper.Map<UserDto>(user);
            return ApiResponse<UserDto>.CreateSuccess(userDto, "User registered successfully.");
        }
        #endregion

        #region Find user by Email
        public async Task<ApiResponse<UserDto>> FindUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return ApiResponse<UserDto>.CreateFail("User not found.");

            var userDto = _mapper.Map<UserDto>(user);
            return ApiResponse<UserDto>.CreateSuccess(userDto, "User retrieved successfully.");
        }
        #endregion

        #region Get All Users
        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            if (!users.Any())
                return ApiResponse<IEnumerable<UserDto>>.CreateFail("No users found.");

            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return ApiResponse<IEnumerable<UserDto>>.CreateSuccess(userDtos, "Users retrieved successfully.");
        }
        #endregion

        #region Get Users By Role
        public async Task<ApiResponse<IEnumerable<UserDto>>> GetUsersByRole(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
                return ApiResponse<IEnumerable<UserDto>>.CreateFail("Role does not exist.");

            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
            if (!usersInRole.Any())
                return ApiResponse<IEnumerable<UserDto>>.CreateFail("No users found in this role.");

            var userDtos = _mapper.Map<IEnumerable<UserDto>>(usersInRole);
            return ApiResponse<IEnumerable<UserDto>>.CreateSuccess(userDtos, $"Users in role '{roleName}' retrieved successfully.");
        }
        #endregion

        #region Delete User By Email
        public async Task<ApiResponse<string>> DeleteUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return ApiResponse<string>.CreateFail("User not found.");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return ApiResponse<string>.CreateFail($"User deletion failed: {errors}");
            }

            return ApiResponse<string>.CreateSuccess(null, "User deleted successfully.");
        }
        #endregion

        #region Generate Token
        private async Task<string> GenerateToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            }.Union(userClaims).Union(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = creds,
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
