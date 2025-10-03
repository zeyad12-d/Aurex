using Microsoft.EntityFrameworkCore;

namespace Aurex_API.Extenenes
{
    public static class Extensions
    {
        // sql connection string
      public static void AddSqlConnection(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<Aurex_Infrastructure.Data.AurexDBcontext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
        }

    }
}
