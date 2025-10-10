
using Aurex_API.Extenenes;
using System.Threading.Tasks;

namespace Aurex_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Extentsion  config
            builder.Services.AddSqlConnection(builder.Configuration);
            builder.Services.AddIdentityConfiguration();
            builder.Services.AddSwaggerConfiguration();
            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.Services.AddAutoMapperConfigration();
            builder.Services.AddFactoryPattern();
            builder.Services.AddServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            
            app.UseAuthorization();
           

            app.MapControllers();
         /*   using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var configuration = services.GetRequiredService<IConfiguration>();
                    await AccountInitializer.Initialize(services,builder.Configuration);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
         */
            app.Run();
        }
    }
}
