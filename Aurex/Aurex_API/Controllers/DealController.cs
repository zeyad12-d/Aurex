using Aurex_Core.DTO.DealDtos;
using Aurex_Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aurex_API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DealController : ControllerBase
    {
        /// <summary>
        /// Manager that provides access to application services used by this controller.
        /// </summary>
        private readonly IServicesManager _ServicesManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DealController"/> class.
        /// </summary>
        /// <param name="servicesManager">An <see cref="IServicesManager"/> implementation that provides access to domain services.</param>
        public DealController(IServicesManager servicesManager)
        {
            _ServicesManager = servicesManager;
        }
        
        /// <summary>
        /// Creates a new deal.
        /// </summary>
        /// <param name="createDealDto">The data transfer object containing details required to create the deal.</param>
        /// <returns>An <see cref="IActionResult"/> containing the operation result. Returns 200 OK on success, 400 Bad Request on failure.</returns>
        [HttpPost("CreateDeal")]
        public async Task<IActionResult> CreateDeal(createDealDto createDealDto)
        {
            var result = await _ServicesManager.DealsService.CreatedAsync(createDealDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Retrieves all deals.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the operation result with the list of deals on success.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ServicesManager.DealsService.GetAllAsync(); 
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Retrieves a deal by its identifier.
        /// </summary>
        /// <param name="Id">The identifier of the deal to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> with the deal details on success or 400 Bad Request on failure.</returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _ServicesManager.DealsService.GetByIdAsync(Id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        } 

        /// <summary>
        /// Updates an existing deal.
        /// </summary>
        /// <param name="updateDealDto">The data transfer object containing updated deal information.</param>
        /// <returns>An <see cref="IActionResult"/> indicating success (200) or failure (400) of the update operation.</returns>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateDealDto updateDealDto)
        {
            var result = await _ServicesManager.DealsService.UpdateDealDtoAsync(updateDealDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Deletes a deal by its identifier.
        /// </summary>
        /// <param name="Id">The identifier of the deal to delete.</param>
        /// <returns>An <see cref="IActionResult"/> indicating success (200) or failure (400) of the delete operation.</returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _ServicesManager.DealsService.DeleteDealDtoAsync(Id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
