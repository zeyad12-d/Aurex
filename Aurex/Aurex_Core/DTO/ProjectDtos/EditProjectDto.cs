using Aurex_Core.Entites;
using System.ComponentModel.DataAnnotations;

namespace Aurex_Core.DTO.ProjectDtos
{
    public record EditProjectDto
    {
        
        public string? Description { get; set; } = String.Empty;
       
        public string? Name { get; set; } = String.Empty;
        
        public decimal? Amount { get; set; }
     
        public int? Probability { get; set; }

       
        public DateTime? DeadlineDate { get; set; }
       
        public ProjectStatus? Status { get; set; }
      
        public NegotiationStage? Negotiation { get; set; }
    }
}
