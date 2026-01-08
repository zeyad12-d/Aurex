using Aurex_Core.Entites;

namespace Aurex_Core.DTO.ClientDtos
{
    public record UpdateClientDto
    {
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Company { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;

        public ClientStatus? Status { get; set; } = ClientStatus.Prospect;
    }
}
