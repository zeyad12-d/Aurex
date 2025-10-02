namespace Aurex_Core.Entites
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        // Navigation property
        public ICollection<Employee>? Employees { get; set; }
    }
}