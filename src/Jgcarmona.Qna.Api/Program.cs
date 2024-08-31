
using Jgcarmona.Qna.Api.Common.Extensions;
using Jgcarmona.Qna.Api.Extensions;
using Jgcarmona.Qna.Application.Initialization;
using Jgcarmona.Qna.Application.Services;
using Jgcarmona.Qna.Common.Configuration.Configuration;
using Jgcarmona.Qna.Common.Converters;
using Jgcarmona.Qna.Domain.Services;
using Jgcarmona.Qna.Infrastructure.Extensions;
using Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Extensions;
using Jgcarmona.Qna.Infrastructure.Persistence.Sql.Extensions;
using Jgcarmona.Qna.Infrastructure.Persistence.Sql.Services;
using Serilog;
using System.Text.Json;

namespace Jgcarmona.Qna.Api
{
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

            builder.Services.Configure<FeatureFlags>(builder.Configuration.GetSection("FeatureFlags"));
            builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));

            builder.Host.UseSerilog((context, loggerConfig) =>
                loggerConfig.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            // Configure services
            builder.Services.AddDatabaseConfiguration(builder.Configuration);
            builder.Services.AddMongoDb(builder.Configuration);
            builder.Services.AddCommandRepositories();
            builder.Services.AddQueryRepositories();
            builder.Services.AddMediatRConfiguration();
            builder.Services.AddSwaggerConfiguration();
            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.Services.AddEventDispatcher(builder.Configuration);

            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IMonikerService, MonikerService>();
            builder.Services.AddScoped<DatabaseInitializer>();
            builder.Services.AddSingleton(new JsonSerializerOptions
            {
                Converters = { new UlidJsonConverter() }
            });

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

}
