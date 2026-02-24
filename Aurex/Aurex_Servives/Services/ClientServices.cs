using Aurex_Core.ApiHelper;
using Aurex_Core.DTO.ClientDtos;
using Aurex_Core.Entites;
using Aurex_Core.Interfaces;
using Aurex_Core.Interfaces.ModelInterfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Aurex_Services.Services
{
    public class ClientServices : IClientService
    {
           readonly IUnitOfWork _unitOfWork;
           readonly IMapper _mapper;
        private IGenericRepository<Client> _repository
            => _unitOfWork.Repository<Client>();


        public ClientServices( IUnitOfWork unit , IMapper mapper  )
        {
            _unitOfWork = unit;
            _mapper = mapper;

        }

        #region Get All Clients (Paginated) 
        public async Task<ApiResponse<PagedResult<ClientResponseDto>>> GetAllClientAsync(int PageNumber, int PageSize)
        {
            PageNumber = PageNumber <= 0 ? 1 : PageNumber;
            PageSize = PageSize <= 0 ? 12 : PageSize;

            var repo = _unitOfWork.Repository<Client>();
            var query = repo.GetQueryable();

            var totalCount = await query.CountAsync();
            if (totalCount == 0)
                return ApiResponse<PagedResult<ClientResponseDto>>.CreateFail("No clients found.");

            var clients = await query.OrderBy(c => c.Id)
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var clientDtos = _mapper.Map<IEnumerable<ClientResponseDto>>(clients);

            var pagedResult = new PagedResult<ClientResponseDto>(

                clientDtos,
                PageNumber,
                PageSize,
                totalCount
            );
            return ApiResponse<PagedResult<ClientResponseDto>>.CreateSuccess(pagedResult, "Clients retrieved successfully.");
            
        }

        #endregion

        #region Get Client By Id  
        public async Task<ApiResponse<ClientResponseDto?>> GetClientByIdAsync(int clientId)
        {
            try
            {
                var repo = _unitOfWork.Repository<Client>();
                var client = await repo.GetByIdAsync(clientId); 
                if(client == null) 
                    return ApiResponse<ClientResponseDto?>.CreateFail("Client not found."); 

                var ClientDto = _mapper.Map<ClientResponseDto>(client); 

                return ApiResponse<ClientResponseDto?>.CreateSuccess(ClientDto, "Client retrieved successfully.");

            }
            catch( Exception ex)
            {
                return ApiResponse <ClientResponseDto?>.CreateFail($"An error occurred: {ex.Message}");
            }

        }
        #endregion


        #region create Client 
        public async Task<ApiResponse<ClientResponseDto>> CreateClientsAsync(CreateClientDto createClientDto)
        {
            try
            {
                if (createClientDto == null)
                    return ApiResponse<ClientResponseDto>.CreateFail("Client is Empty");

               var client= _mapper.Map<Client>(createClientDto);

               await _repository.AddAsync(client);
               await _unitOfWork.CompleteAsync();

                return ApiResponse<ClientResponseDto>.CreateSuccess(_mapper.Map<ClientResponseDto>(client));


            }
            catch(Exception EX)
            {
                return ApiResponse<ClientResponseDto>.CreateFail($"An error occurred: {EX.Message}");
            }
        }
        #endregion

        #region Delete Client  
        public async Task<ApiResponse<ClientResponseDto>> DeleteClientAsync(int clientId)
        {
            if (clientId <= 0)
                return ApiResponse<ClientResponseDto>.CreateFail("UnValid Id");

           var client = await _repository.GetByIdAsync(clientId);

            if (client == null)
                return ApiResponse<ClientResponseDto>.CreateFail(" Client Not Found ");

             _repository.Delete(client);
            await _unitOfWork.CompleteAsync();
            return ApiResponse<ClientResponseDto>.CreateSuccess(_mapper.Map<ClientResponseDto>(client), "Client Deleted Succeccfully");
        }
        #endregion


    
        #region Get Client Deals 
        public async Task<ApiResponse<ClientDealsDto?>> GetClientDealsAsync(int clientId)
        {
           var client = await _repository.GetQueryable().Include(c => c.Deals)
                .FirstOrDefaultAsync(c => c.Id == clientId);
            if (client == null)
                return ApiResponse<ClientDealsDto?>.CreateFail("Client Not Found");

            var ClientDto = _mapper.Map<ClientDealsDto>(client);

            return ApiResponse<ClientDealsDto?>.CreateSuccess(ClientDto, "Client Retrived Succeccfully");
        }




        #endregion


        #region Client Services









        public Task<ApiResponse<int>> GetTotalClientsCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<PagedResult<ClientResponseDto>>> SearchClientAsync(string searchTerm, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ClientResponseDto>> UpdateClientAsync(int clientId, UpdateClientDto updateClientDto)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
