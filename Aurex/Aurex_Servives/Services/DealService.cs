using Aurex_Core.DTO.DealDtos;
using Aurex_Core.Entites;
using Aurex_Core.Interfaces;
using Aurex_Core.Interfaces.ModleInterFaces;
using Aurex_Services.ApiHelper;
using AutoMapper;

namespace Aurex_Services.Services;

public class DealService : IDealsService
{
    private readonly IMapper _mapper; 
    private  readonly IUnitOfWork _unitOfWork;
    
    
    private IGenericRepository<Deal> DealRepository
        => _unitOfWork.Repository<Deal>();
 
    

    public DealService(IMapper mapper, IUnitOfWork unitOfWork )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
   
    
    public async Task<ApiResponse<IEnumerable<DealResponseDto>>> GetAllAsync()
    {
        var deals = await DealRepository.GetAllAsync();

        var dealDtos = _mapper.Map<List<DealResponseDto>>(deals);

        return ApiResponse<IEnumerable<DealResponseDto>>
            .CreateSuccess(dealDtos);
    }
    
    
    public async Task<ApiResponse<DealResponseDto>> GetByIdAsync(int id)
    {
        if (id <= 0)
            return ApiResponse<DealResponseDto>.CreateFail("Invalid id");

        var deal = await DealRepository.GetByIdAsync(id);

        if (deal == null)
            return ApiResponse<DealResponseDto>.CreateFail("Deal not found");

        return ApiResponse<DealResponseDto>
            .CreateSuccess(_mapper.Map<DealResponseDto>(deal));
    }

    public async Task<ApiResponse<DealResponseDto>> CreatedAsync(createDealDto dto)
    {
        var deal = _mapper.Map<Deal>(dto);

        await DealRepository.AddAsync(deal);
        await _unitOfWork.CompleteAsync(); 

        return ApiResponse<DealResponseDto>
            .CreateSuccess(_mapper.Map<DealResponseDto>(deal));
    }
    
    
    public async Task<ApiResponse<DealResponseDto>> UpdateDealDtoAsync(UpdateDealDto updateDealDto)
    {
        var deal = await DealRepository.GetByIdAsync(updateDealDto.Id); 
        if (deal == null)
            return ApiResponse<DealResponseDto>.CreateFail("Deal not found");
        _mapper.Map(updateDealDto, deal);
         DealRepository.Update(deal); 
         await _unitOfWork.CompleteAsync();
         return ApiResponse<DealResponseDto>
             .CreateSuccess(_mapper.Map<DealResponseDto>(deal));
         
    }

    public async Task<ApiResponse<DealResponseDto>> DeleteDealDtoAsync(int id)
    {
        if( id <= 0) 
            return ApiResponse<DealResponseDto>.CreateFail("Invalid id"); 
        var deal = await DealRepository.GetByIdAsync(id); 
        if (deal == null) 
            return ApiResponse<DealResponseDto>.CreateFail("Deal not found"); 
        DealRepository.Delete(deal);
        await _unitOfWork.CompleteAsync();
        return ApiResponse<DealResponseDto>
            .CreateSuccess(_mapper.Map<DealResponseDto>(deal),"Deal Deleted Successfully");
    }
}