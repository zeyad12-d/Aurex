using Aurex_Core.Entites;
using System.ComponentModel.DataAnnotations;

namespace Aurex_Core.DTO.ProjectDtos
{
    public record CreateProjectDto
    {
        [Required(ErrorMessage = "Description is required."), MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")
         , MinLength(10, ErrorMessage = "Description must be at least 10 characters long.")]
         
        public string Description { get; set; } = String.Empty;
        [Required(ErrorMessage ="Name is required."), MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")
         , MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]

        public string Name { get; set; } = String.Empty;
            [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
            public decimal Amount { get; set; }
            [Range(0, 100, ErrorMessage = "Probability must be between 0 and 100.")]
            public int Probability { get; set; }

        [Required(ErrorMessage = "DeadlineDate is required.")
         , DataType(DataType.Date, ErrorMessage = "DeadlineDate must be a valid date.")]
        public DateTime DeadlineDate { get; set; }
        [Required(ErrorMessage = "Status is required.")]

        public ProjectStatus Status { get; set; }
        [Required(ErrorMessage = "Negotiation stage is required.")]
        public NegotiationStage Negotiation { get; set; }

    }
}
