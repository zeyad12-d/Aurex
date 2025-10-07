## Aurex CRM

Aurex is a Customer Relationship Management (CRM) system built with ASP.NET Core 8 Web API, Entity Framework Core, and ASP.NET Identity with role-based authorization.

### Tech stack
- **Backend**: ASP.NET Core 8 Web API
- **Auth**: ASP.NET Identity + JWT Bearer
- **Data**: EF Core (SQL Server)
- **Docs**: Swagger/OpenAPI
- **Mapping**: AutoMapper

### Solution structure
- `Aurex_API` — Web API (startup project)
- `Aurex_Infrastructure` — EF Core `DbContext`, migrations, repositories
- `Aurex_Core` — domain entities and interfaces
- `Aurex_Servives` — application services

### Prerequisites
- .NET SDK 8.0+
- SQL Server (local instance or container)

### Configuration
Update `Aurex/Aurex_API/appsettings.json` with your values:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=AurexDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;MultipleActiveResultSets=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AdminUser": {
    "Email": "admin@example.com",
    "UserName": "admin",
    "Password": "Admin#123"
  },
  "JwtSettings": {
    "Issuer": "Aurex",
    "Audience": "Aurex",
    "SecretKey": "change-me-to-a-long-random-secret-at-least-32-chars"
  },
  "AllowedHosts": "*"
}
```

Tip: For local dev, prefer using user-secrets instead of committing secrets to `appsettings.json`.

```bash
dotnet user-secrets init --project Aurex/Aurex_API
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "..." --project Aurex/Aurex_API
dotnet user-secrets set "JwtSettings:SecretKey" "..." --project Aurex/Aurex_API
dotnet user-secrets set "AdminUser:Email" "admin@example.com" --project Aurex/Aurex_API
dotnet user-secrets set "AdminUser:UserName" "admin" --project Aurex/Aurex_API
dotnet user-secrets set "AdminUser:Password" "Admin#123" --project Aurex/Aurex_API
```

### Database
Install EF Core CLI if needed:

```bash
dotnet tool install --global dotnet-ef
```

Apply migrations and create the database:

```bash
dotnet ef database update \
  --project Aurex/Aurex_Infrastructure \
  --startup-project Aurex/Aurex_API
```

Create a new migration (when changing the model):

```bash
dotnet ef migrations add <MigrationName> \
  --project Aurex/Aurex_Infrastructure \
  --startup-project Aurex/Aurex_API \
  -o Migrations
```

### (Optional) Seed admin and roles
Ensure `AdminUser` is set in configuration, then enable the seeding block in `Aurex/Aurex_API/Program.cs` (uncomment it), run once to seed, then comment it out again to avoid reseeding on every start.

### Run the API
From the repo root:

```bash
dotnet restore
dotnet run --project Aurex/Aurex_API
```

The API starts with Swagger UI at:
- HTTP: `http://localhost:5156/swagger`
- HTTPS: `https://localhost:7186/swagger`

### Swagger + JWT
- Use the Swagger UI "Authorize" button.
- Enter: `Bearer <your-jwt-token>` as the value when prompted.

Note: Account/auth endpoints will appear in Swagger once corresponding controllers are implemented.

### Troubleshooting
- **Cannot connect to SQL Server**: Check port, credentials, and add `TrustServerCertificate=True;MultipleActiveResultSets=true;` to the connection string for local dev.
- **EF CLI missing**: Install `dotnet-ef` as shown above.
- **401 Unauthorized**: Ensure JWT `Issuer`, `Audience`, and `SecretKey` match between token issuer and API configuration.

