using Aurex_Core.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aurex_API.Extenenes
{
    public static class Extensions
    {
        
        #region AddSql
        public static void AddSqlConnection(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<Aurex_Infrastructure.Data.AurexDBcontext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
       }
        #endregion
        
        #region Identity
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
            })
             .AddEntityFrameworkStores<Aurex_Infrastructure.Data.AurexDBcontext>()
            .AddDefaultTokenProviders();
        }
        #endregion

    }
}
