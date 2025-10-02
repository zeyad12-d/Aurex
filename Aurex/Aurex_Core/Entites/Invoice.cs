namespace Aurex_Core.Entites
{
    public class Invoice
    {
        public int Id { get; set; }
        public string VendorName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public InvoiceStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;

        public int DealId { get; set; }
        public Deal Deal { get; set; }
    }
    public enum InvoiceStatus
    {
        Pending,
        Prefund,
        Approved,
        Rejected
    }
}