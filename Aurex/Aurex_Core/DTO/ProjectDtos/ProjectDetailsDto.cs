namespace Aurex_Core.DTO.ProjectDtos
{
    public record ProjectDetailsDto
    {
        public DateTime DeadlineDate { get; set; }
        public string Description { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public string? ImageUrl { get; set; } 
    }
}
