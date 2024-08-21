using Jgcarmona.Qna.Api.Common.Extensions;
using Jgcarmona.Qna.Api.Web.Extensions;
using Jgcarmona.Qna.Application.Features.Auth;
using Jgcarmona.Qna.Application.Features.Users;
using Jgcarmona.Qna.Application.Initialization;
using Jgcarmona.Qna.Application.Services;
using Jgcarmona.Qna.Domain.Abstract.Services;
using Jgcarmona.Qna.Persistence.EntityFramework.Extensions;
using Serilog;

namespace Jgcarmona.Qna.Api.Web;

public class Program
{
    private static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();


        builder.Host.UseSerilog((context, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        // Configure services
        builder.Services.AddDatabaseConfiguration(builder.Configuration);
        builder.Services.AddRepositories();
        builder.Services.AddMediatRConfiguration();
        builder.Services.AddSwaggerConfiguration();
        builder.Services.AddJwtAuthentication(builder.Configuration);

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
        builder.Services.AddScoped<DatabaseInitializer>();

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseRequestContextLogging();

        await app.Services.InitializeDatabaseAsync(builder.Configuration);

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "QnA API v1");
                c.RoutePrefix = string.Empty;
                c.OAuthUsePkce();
            });
        }

        app.MapGet("/", context =>
        {
            context.Response.Redirect("/swagger");
            return Task.CompletedTask;
        });

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
