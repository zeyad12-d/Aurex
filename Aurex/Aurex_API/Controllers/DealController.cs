using Aurex_Core.DTO.AccountDtos;
using Aurex_Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aurex_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IServicesManager _ServicesManager;

        public AccountController(IServicesManager servicesManager)
        {
            _ServicesManager = servicesManager;
        }

        /// <summary>
        /// Authenticates a user with the provided credentials.
        /// </summary>
        /// <param name="loginDto">The login credentials.</param>
        /// <returns>Returns a success response with user info or a bad request if authentication fails.</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _ServicesManager.AccountServices.Login(loginDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Registers a new user with the provided information.
        /// </summary>
        /// <param name="registerDto">The registration details.</param>
        /// <returns>Returns a success response with user info or a bad request if registration fails.</returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _ServicesManager.AccountServices.Register(registerDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Logs out the user with the specified email.
        /// </summary>
        /// <param name="email">The email of the user to log out.</param>
        /// <returns>Returns a success response or a bad request if logout fails.</returns>
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] string email)
        {
            var result = await _ServicesManager.AccountServices.Logout(email);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Finds a user by their email address.
        /// </summary>
        /// <param name="email">The email address to search for.</param>
        /// <returns>Returns user information if found, otherwise a bad request.</returns>
        [HttpGet("User/Email")]
        public async Task<IActionResult> FindUserByEmail([FromQuery] string email)
        {
            var result = await _ServicesManager.AccountServices.FindUserByEmail(email);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>Returns a list of all users or a bad request if retrieval fails.</returns>
        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _ServicesManager.AccountServices.GetAllUsers();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Retrieves users by their role.
        /// </summary>
        /// <param name="roleName">The name of the role.</param>
        /// <returns>Returns a list of users with the specified role or a bad request if retrieval fails.</returns>
        [HttpGet("Users/Role")]
        public async Task<IActionResult> GetUserByRole(string roleName)
        {
            var result = await _ServicesManager.AccountServices.GetUsersByRole(roleName);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Deletes a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user to delete.</param>
        /// <returns>Returns a success response if deleted, otherwise a bad request.</returns>
        [HttpDelete("User/Email")]
        public async Task<IActionResult> DeleteUserByEmail([FromQuery] string email)
        {
            var result = await _ServicesManager.AccountServices.DeleteUserByEmail(email);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
