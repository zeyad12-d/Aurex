using Aurex_Core.Entites;
using Microsoft.AspNetCore.Identity;
using Aurex_Infrastructure.Data;
using TaskStatus = Aurex_Core.Entites.TaskStatus;

public class AccountInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var context = serviceProvider.GetRequiredService<AurexDBcontext>();

        // Initialize roles
        await InitializeRoles(roleManager);

        // Initialize admin user
        await InitializeAdminUser(userManager, configuration);

        // Initialize fake data
        await InitializeFakeData(context, userManager);
    }

    private static async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Admin", "Employee", "SalesPerson", "AccountPerson" };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    private static async Task InitializeAdminUser(UserManager<User> userManager, IConfiguration configuration)
    {
        var adminConfig = configuration.GetSection("AdminUser");
        string adminEmail = adminConfig["Email"];
        string adminName = adminConfig["UserName"];
        string adminPassword = adminConfig["Password"];
        
        // check if data == null or empty
        if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminName) || string.IsNullOrEmpty(adminPassword))
        {
            throw new Exception("Admin user configuration is missing or incomplete in appsettings.json.");
        }

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            var newAdminUser = new User
            {
                UserName = adminName,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(newAdminUser, adminPassword);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(newAdminUser, "Admin");
            else
                foreach (var error in result.Errors)
                    Console.WriteLine($"Error: {error.Description}");
        }
    }

    private static async Task InitializeFakeData(AurexDBcontext context, UserManager<User> userManager)
    {
        // Check if data already exists
        if (context.Departments.Any())
            return;

        // Create Departments
        var departments = new List<Department>
        {
            new Department { Name = "Sales" },
            new Department { Name = "Marketing" },
            new Department { Name = "Development" },
            new Department { Name = "HR" },
            new Department { Name = "Finance" },
            new Department { Name = "Operations" }
        };

        context.Departments.AddRange(departments);
        await context.SaveChangesAsync();

        // Create Users and Employees
        var users = new List<User>();
        var employees = new List<Employee>();

        var employeeData = new[]
        {
            new { Name = "John Smith", Position = "Sales Manager", Email = "john.smith@aurex.com", Department = "Sales", Score = 95 },
            new { Name = "Sarah Johnson", Position = "Marketing Specialist", Email = "sarah.johnson@aurex.com", Department = "Marketing", Score = 88 },
            new { Name = "Mike Chen", Position = "Senior Developer", Email = "mike.chen@aurex.com", Department = "Development", Score = 92 },
            new { Name = "Emily Davis", Position = "HR Manager", Email = "emily.davis@aurex.com", Department = "HR", Score = 90 },
            new { Name = "David Wilson", Position = "Financial Analyst", Email = "david.wilson@aurex.com", Department = "Finance", Score = 87 },
            new { Name = "Lisa Brown", Position = "Operations Coordinator", Email = "lisa.brown@aurex.com", Department = "Operations", Score = 85 },
            new { Name = "Alex Rodriguez", Position = "Sales Representative", Email = "alex.rodriguez@aurex.com", Department = "Sales", Score = 82 },
            new { Name = "Maria Garcia", Position = "Marketing Manager", Email = "maria.garcia@aurex.com", Department = "Marketing", Score = 91 },
            new { Name = "James Taylor", Position = "Junior Developer", Email = "james.taylor@aurex.com", Department = "Development", Score = 78 },
            new { Name = "Jennifer Lee", Position = "HR Specialist", Email = "jennifer.lee@aurex.com", Department = "HR", Score = 83 }
        };

        foreach (var emp in employeeData)
        {
            var user = new User
            {
                UserName = emp.Email,
                Email = emp.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "Password123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Employee");
                users.Add(user);

                var department = departments.First(d => d.Name == emp.Department);
                var employee = new Employee
                {
                    Name = emp.Name,
                    Position = emp.Position,
                    HireDate = DateTime.UtcNow.AddDays(-Random.Shared.Next(30, 1095)), // Random hire date within last 3 years
                    ImageUrl = $"https://ui-avatars.com/api/?name={emp.Name.Replace(" ", "+")}&background=random",
                    Score = emp.Score,
                    UserId = user.Id,
                    DepartmentId = department.Id
                };

                employees.Add(employee);
            }
        }

        context.Employees.AddRange(employees);
        await context.SaveChangesAsync();

        // Create Clients
        var clients = new List<Client>
        {
            new Client { Name = "TechCorp Solutions", Email = "contact@techcorp.com", Phone = "+1-555-0101", Company = "TechCorp Solutions", Location = "New York, NY", Status = ClientStatus.Active },
            new Client { Name = "Global Industries", Email = "info@globalind.com", Phone = "+1-555-0102", Company = "Global Industries", Location = "Los Angeles, CA", Status = ClientStatus.Active },
            new Client { Name = "Innovation Labs", Email = "hello@innovationlabs.com", Phone = "+1-555-0103", Company = "Innovation Labs", Location = "San Francisco, CA", Status = ClientStatus.Prospect },
            new Client { Name = "Enterprise Systems", Email = "sales@enterprisesys.com", Phone = "+1-555-0104", Company = "Enterprise Systems", Location = "Chicago, IL", Status = ClientStatus.Active },
            new Client { Name = "Digital Dynamics", Email = "contact@digitaldynamics.com", Phone = "+1-555-0105", Company = "Digital Dynamics", Location = "Austin, TX", Status = ClientStatus.Inactive },
            new Client { Name = "Future Technologies", Email = "info@futuretech.com", Phone = "+1-555-0106", Company = "Future Technologies", Location = "Seattle, WA", Status = ClientStatus.Prospect },
            new Client { Name = "Smart Solutions", Email = "hello@smartsolutions.com", Phone = "+1-555-0107", Company = "Smart Solutions", Location = "Boston, MA", Status = ClientStatus.Active },
            new Client { Name = "NextGen Corp", Email = "contact@nextgen.com", Phone = "+1-555-0108", Company = "NextGen Corp", Location = "Denver, CO", Status = ClientStatus.Prospect }
        };

        context.Clients.AddRange(clients);
        await context.SaveChangesAsync();

        // Create Deals
        var deals = new List<Deal>();
        var currencies = Enum.GetValues<Currency>();
        var dealStatuses = Enum.GetValues<DealStatus>();

        for (int i = 0; i < 15; i++)
        {
            var deal = new Deal
            {
                Amount = Random.Shared.Next(10000, 500000),
                Currency = currencies[Random.Shared.Next(currencies.Length)],
                Endtime = DateTime.UtcNow.AddDays(Random.Shared.Next(30, 365)),
                Status = dealStatuses[Random.Shared.Next(dealStatuses.Length)],
                ClientId = clients[Random.Shared.Next(clients.Count)].Id,
                EmployeeId = employees[Random.Shared.Next(employees.Count)].Id
            };
            deals.Add(deal);
        }

        context.Deals.AddRange(deals);
        await context.SaveChangesAsync();

        // Create Projects
        var projects = new List<Project>();
        var projectStatuses = Enum.GetValues<ProjectStatus>();
        var negotiationStages = Enum.GetValues<NegotiationStage>();

        for (int i = 0; i < 12; i++)
        {
            var project = new Project
            {
                Amount = Random.Shared.Next(50000, 1000000),
                Probability = Random.Shared.Next(10, 100),
                DeadlineDate = DateTime.UtcNow.AddDays(Random.Shared.Next(60, 730)),
                Status = projectStatuses[Random.Shared.Next(projectStatuses.Length)],
                Negotiation = negotiationStages[Random.Shared.Next(negotiationStages.Length)],
                ImageUrl = $"https://picsum.photos/300/200?random={i}",
                DealId = deals[Random.Shared.Next(deals.Count)].Id,
                TeamLeaderId = employees[Random.Shared.Next(employees.Count)].Id
            };
            projects.Add(project);
        }

        context.Projects.AddRange(projects);
        await context.SaveChangesAsync();

        // Create Employee Projects (Many-to-Many relationship)
        var employeeProjects = new List<EmployeeProject>();
        var roles = new[] { "Developer", "Designer", "Project Manager", "QA Tester", "Business Analyst", "Technical Lead" };

        foreach (var project in projects)
        {
            var projectEmployees = employees.OrderBy(x => Random.Shared.Next()).Take(Random.Shared.Next(2, 6));
            foreach (var employee in projectEmployees)
            {
                employeeProjects.Add(new EmployeeProject
                {
                    EmployeeId = employee.Id,
                    ProjectId = project.Id,
                    Role = roles[Random.Shared.Next(roles.Length)],
                    AssignedAt = DateTime.UtcNow.AddDays(-Random.Shared.Next(1, 90))
                });
            }
        }

        context.EmployeeProjects.AddRange(employeeProjects);
        await context.SaveChangesAsync();

        // Create Employee Tasks
        var tasks = new List<EmployeeTask>();
        var taskStatuses = Enum.GetValues<TaskStatus>();
        var taskPriorities = Enum.GetValues<TaskPriority>();

        var taskNames = new[]
        {
            "Review project requirements", "Update documentation", "Code review", "Client meeting preparation",
            "Database optimization", "UI/UX design", "Testing implementation", "Bug fixes",
            "Performance analysis", "Security audit", "Deployment preparation", "Training session",
            "Report generation", "Budget analysis", "Team coordination", "Quality assurance"
        };

        for (int i = 0; i < 50; i++)
        {
            var task = new EmployeeTask
            {
                Name = taskNames[Random.Shared.Next(taskNames.Length)],
                Notes = $"Task notes for {taskNames[Random.Shared.Next(taskNames.Length)]}",
                Date = DateTime.UtcNow.AddDays(-Random.Shared.Next(1, 30)),
                Status = taskStatuses[Random.Shared.Next(taskStatuses.Length)],
                Priority = taskPriorities[Random.Shared.Next(taskPriorities.Length)],
                EmployeeId = employees[Random.Shared.Next(employees.Count)].Id
            };
            tasks.Add(task);
        }

        context.EmployeeTasks.AddRange(tasks);
        await context.SaveChangesAsync();

        // Create Invoices
        var invoices = new List<Invoice>();
        var invoiceStatuses = Enum.GetValues<InvoiceStatus>();
        var vendorNames = new[]
        {
            "Cloud Services Inc", "Software Solutions Ltd", "Tech Hardware Corp", "Digital Marketing Pro",
            "Security Systems Co", "Data Analytics LLC", "Network Infrastructure Inc", "Mobile Apps Solutions"
        };

        for (int i = 0; i < 20; i++)
        {
            var invoice = new Invoice
            {
                VendorName = vendorNames[Random.Shared.Next(vendorNames.Length)],
                Email = $"vendor{i}@example.com",
                UnitPrice = Random.Shared.Next(100, 10000),
                Status = invoiceStatuses[Random.Shared.Next(invoiceStatuses.Length)],
                Notes = $"Invoice for services provided - Invoice #{i + 1000}",
                DealId = deals[Random.Shared.Next(deals.Count)].Id
            };
            invoices.Add(invoice);
        }

        context.Invoices.AddRange(invoices);
        await context.SaveChangesAsync();

        // Create Activities
        var activities = new List<Activity>();
        var activityTypes = Enum.GetValues<ActivityType>();

        var activityNotes = new[]
        {
            "Product demonstration completed", "Client requirements discussion", "Follow-up meeting scheduled",
            "Contract negotiation in progress", "Technical consultation provided", "Project status update",
            "Budget review meeting", "Timeline discussion", "Quality assurance review", "Training session conducted"
        };

        for (int i = 0; i < 30; i++)
        {
            var activity = new Activity
            {
                Type = activityTypes[Random.Shared.Next(activityTypes.Length)],
                Date = DateTime.UtcNow.AddDays(-Random.Shared.Next(1, 60)),
                Notes = activityNotes[Random.Shared.Next(activityNotes.Length)],
                EmployeeId = employees[Random.Shared.Next(employees.Count)].Id
            };
            activities.Add(activity);
        }

        context.Activities.AddRange(activities);
        await context.SaveChangesAsync();

        Console.WriteLine("Fake data seeding completed successfully!");
    }
}
