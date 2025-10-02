namespace Aurex_Core.Entites
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public int Score { get; set; }

        // Navigation property
        public string UserId { get; set; } = string.Empty;

        public User? User { get; set; }

        public int DepartmentId { get; set; }

        public Department? Department { get; set; }

        // icollection
        public ICollection<EmployeeTask>? EmployeeTasks { get; set; }

        public ICollection<Deal> Deals { get; set; } = new List<Deal>();

        public ICollection<EmployeeProject>? EmployeeProjects { get; set; }


        public ICollection<Project> LeadingProjects { get; set; } = new List<Project>();


    }
}