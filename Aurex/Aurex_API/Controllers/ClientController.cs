using Aurex_Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aurex_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IServicesManager _servicesManager;

        public ClientController( IServicesManager servicesManager)
        {
            _servicesManager = servicesManager; 
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClient()
        {
            var result = await _servicesManager.ClientService.GetAllClientAsync(1, 12);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("{id}")] 
        public async Task<IActionResult> GetClientById(int id)
        {
            var result = await _servicesManager.ClientService.GetClientByIdAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        } 
    }
}
