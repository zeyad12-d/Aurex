using Aurex_Core.Entites;
using Aurex_Core.Interfaces;

using Aurex_Core.Interfaces.ModleInterFaces;
using Aurex_Infrastructure.Repositories;
using Aurex_Services.Services;
using Aurex_Services.Services.Factory;
using Aurex_Services.Services.Manager;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
                options.User.RequireUniqueEmail = true;
            })
             .AddEntityFrameworkStores<Aurex_Infrastructure.Data.AurexDBcontext>()
            .AddDefaultTokenProviders();
        }
        #endregion


         # region Swagger

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Aurex API",
                    Version = "v1",
                    Description = "API documentation for Aurex project."
                });

              
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                    c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid JWT token.",
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

              
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            });
        }

        #endregion

        #region JWT
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtSettings = config.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero ,// Optional: Eliminate delay when token expires
                    
                };
            });
        }
        #endregion

        #region UnitOfWork
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        #endregion

        #region AddAutoMapperConfigration
        public static void AddAutoMapperConfigration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Aurex_Services.MapperProfile.AccountProfile).Assembly);
            ;
        }

        #endregion

        #region Factory pattern
        public static void AddFactoryPattern(this IServiceCollection services)
        {
            services.AddScoped<IServiceFactory, ServiceFactory>();
            services.AddScoped<IServicesManager, ServicesManager>();
          
        }
        #endregion

        #region services 
        public static void AddServices(this IServiceCollection services)
        {
            // filters Log information 
            services.AddScoped<LogActivityFilter>();
            //services 
            services.AddScoped<IAccountServices,AccountServices>();
            services.AddScoped<IEmployeeServices, EmployeeServices>();
            services.AddScoped<IDepartmentService,DepartmentServices>();
        }
        #endregion

        #region Cors // development environment
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }
        #endregion
    }
}
