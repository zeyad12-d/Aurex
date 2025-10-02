namespace Aurex_Core.Entites
{
    public class EmployeeTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.New;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;


        // Foreign key
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
    public enum TaskStatus
    {
        New,
        InProgress,
        Completed,
        Cancelled
    }
    public enum TaskPriority
    {
        Low,
        Medium,
        High,
        Critical
    }
}