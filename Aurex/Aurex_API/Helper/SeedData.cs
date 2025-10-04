using Aurex_Core.Entites;
using Microsoft.AspNetCore.Identity;

public class AccountInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Admin", "Employee", "SalesPerson", "AccountPerson" };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole(roleName));
        }

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
}
