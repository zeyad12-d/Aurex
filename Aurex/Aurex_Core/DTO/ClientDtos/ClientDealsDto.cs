namespace Aurex_Core.DTO.ClientDtos
{
    public record  ClientDealsDto
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public List<DealResponse> Deals { get; set; } = new List<DealResponse>();
    }
}
