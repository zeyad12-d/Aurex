using Aurex_Core.ApiHelper;
using Aurex_Core.DTO.ClientDtos;


namespace Aurex_Core.Interfaces.ModelInterfaces
{
    public  interface IClientService
    {
        Task<ApiResponse<ClientDealsDto?>> GetClientDealsAsync(int clientId);
        Task<ApiResponse<ClientResponseDto>> UpdateClientAsync(int clientId, UpdateClientDto updateClientDto);

        Task<ApiResponse<ClientResponseDto>> DeleteClientAsync(int clientId);

        Task<ApiResponse<PagedResult<ClientResponseDto>>> GetAllClientAsync(int PageNumber, int PageSize);

        Task<ApiResponse<int>> GetTotalClientsCountAsync();
        Task<ApiResponse<ClientResponseDto>> CreateClientsAsync(CreateClientDto createClientDto); 
        Task<ApiResponse<ClientResponseDto?>> GetClientByIdAsync(int clientId);
        Task<ApiResponse<PagedResult<ClientResponseDto>>> SearchClientAsync( string searchTerm, int pageNumber, int pageSize); 

    }
}
