namespace Aurex_Core.Entites
{
    public class Deal
    {
       public int Id { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public DateTime Endtime { get; set; }
        public DealStatus Status { get; set; } = DealStatus.Open;

        // Foreign keys
        public int ClientId { get; set; }

        public Client? Client { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public Project? Project { get; set; }
         
        public ICollection<Invoice> invoices { get; set; } = new List<Invoice>();

    }
    public enum DealStatus
    {
        Open,
        InProgress,
        Closed,
        Lost
    }

    public enum Currency
    {
        USD,
        EUR,
        GBP,
        EGP,
        SAR
    }
}