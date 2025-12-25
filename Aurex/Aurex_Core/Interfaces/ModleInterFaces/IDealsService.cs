using Aurex_Core.DTO.DealDtos;
using Aurex_Services.ApiHelper;

namespace Aurex_Core.Interfaces.ModleInterFaces;

public interface IDealsServices
{
    Task<ApiResponse<IEnumerable<DealResponseDto>>> GetAllAsync();
    Task<ApiResponse<DealResponseDto>> GetByIdAsync(int id);
    Task<ApiResponse<DealResponseDto>>CreatedAsync(createDealDto createDealDto); 
    Task<ApiResponse<DealResponseDto>>UpdateDealDtoAsync(UpdateDealDto updateDealDto);
    Task<ApiResponse<DealResponseDto>> DeleteDealDtoAsync(int id);
    
    


}