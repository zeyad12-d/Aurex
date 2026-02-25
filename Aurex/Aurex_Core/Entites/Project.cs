namespace Aurex_Core.Entites
{
    public class Project
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public int Probability { get; set; }     
        public DateTime DeadlineDate { get; set; }
        public ProjectStatus Status { get; set; }
        public NegotiationStage Negotiation { get; set; }
        public string? ImageUrl { get; set; }

        // Foreign keys
        public int DealId { get; set; }
        public Deal Deal { get; set; }
        public int? TeamLeaderId { get; set; }
        public Employee? TeamLeader { get; set; }

        public ICollection<EmployeeProject>? EmployeeProjects { get; set; }

    }
    public enum ProjectStatus
    {
        Planned,
        InProgress,
        Completed,
        Cancelled
    }

    public enum NegotiationStage
    {
        Initial,
        ProposalSent,
        UnderReview,
        FinalAgreement
    }
}