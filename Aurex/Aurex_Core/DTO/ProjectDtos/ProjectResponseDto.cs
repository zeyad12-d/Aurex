using Aurex_Core.Entites;

namespace Aurex_Core.DTO.ProjectDtos
{
    public record ProjectResponseDto
    {
        public decimal Amount { get; set; }
        public int Probability { get; set; }
        public DateTime DeadlineDate { get; set; }
        public ProjectStatus Status { get; set; }
        public NegotiationStage Negotiation { get; set; }
        public string? ImageUrl { get; set; }
    }
}
