using Aurex_Core.ApiHelper;
using Aurex_Core.DTO.DealDtos;


namespace Aurex_Core.Interfaces.ModelInterfaces;

public interface IDealsService
{
    Task<ApiResponse<IEnumerable<DealResponseDto>>> GetAllAsync();
    Task<ApiResponse<DealResponseDto>> GetByIdAsync(int id);
    Task<ApiResponse<DealResponseDto>>CreatedAsync(createDealDto createDealDto); 
    Task<ApiResponse<DealResponseDto>>UpdateDealDtoAsync(UpdateDealDto updateDealDto);
    Task<ApiResponse<DealResponseDto>> DeleteDealDtoAsync(int id);
    
    


}