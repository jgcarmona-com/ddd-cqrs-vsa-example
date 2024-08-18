using Jgcarmona.Qna.Core;
using Jgcarmona.Qna.Infrastructure.Persistence;
using Jgcarmona.Qna.Application.Features.Auth;
using Jgcarmona.Qna.Application.Features.Users;
using Jgcarmona.Qna.Application.Services;
using Jgcarmona.Qna.Application.Initialization;
using Jgcarmona.Qna.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

        // Configure services
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




        // Configure DI:
        builder.Services.AddRepositories();

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
        builder.Services.AddScoped<DatabaseInitializer>();

        // Configure auth
        var key = builder.Configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key), "JWT Key is not configured properly.");
        }

        var keyBytes = Encoding.ASCII.GetBytes(key);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                };
            });
        builder.Services.AddAuthorization();

        // Cargar Controllers
        builder.Services.AddControllers();

        // Configure Swagger with Constants
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = Constants.Title,
                Version = "v1",
                Description = Constants.Description,
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = Constants.Contact["name"],
                    Url = new Uri(Constants.Contact["url"]),
                    Email = Constants.Contact["email"]
                },
                License = new Microsoft.OpenApi.Models.OpenApiLicense
                {
                    Name = Constants.LicenseInfo["name"],
                    Url = new Uri(Constants.LicenseInfo["url"])
                }
            });
        });

        var app = builder.Build();

        // Initialize database
        using (var scope = app.Services.CreateScope())
        {
            var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
            await initializer.SeedAsync();
        }


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "QnA API v1");
                c.RoutePrefix = string.Empty; // Esto carga Swagger en la raíz '/'
            });
        }
        app.MapGet("/", context =>
        {
            context.Response.Redirect("/swagger");
            return Task.CompletedTask;
        });
        // app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}