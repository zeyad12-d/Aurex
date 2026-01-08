using Aurex_Core.Entites;

namespace Aurex_Core.DTO.ClientDtos
{
    public  record DealResponse
    {
        public int Id { get; init; }
        public decimal Amount { get; init; }

        public DateTime Endtime { get; init; }

        public DealStatus Status { get; init; }
    }
}
