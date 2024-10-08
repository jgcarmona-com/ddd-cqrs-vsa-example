using DotNetEnv;
using Jgcarmona.Qna.Common.Configuration;
using Jgcarmona.Qna.Common.Converters;
using Jgcarmona.Qna.Infrastructure.Extensions;
using Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Extensions;
using Jgcarmona.Qna.Services.Common;
using Serilog;
using System.Text.Json;

namespace Jgcarmona.Qna.Services.SyncService;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Load environment variables from .env file
        Env.Load();
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        try
        {
            Log.Information("Starting SyncService");
            var host = CreateHostBuilder(args).Build();

            // Initialize MongoDB connection before running the host
            await host.Services.InitializeMongoDbAsync();

            await host.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "SyncService terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                      .AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Add secrets provider extension
                services.AddCustomSecrets(hostContext.Configuration);

                services.Configure<FeatureFlags>(hostContext.Configuration.GetSection("FeatureFlags"));
                services.Configure<RabbitMQSettings>(hostContext.Configuration.GetSection("RabbitMQSettings"));

                services.AddMongoDb(hostContext.Configuration);
                services.AddSingleton(new JsonSerializerOptions
                {
                    Converters = { new UlidJsonConverter() }
                });
                services.AddMessagingListener(hostContext.Configuration);
                services.AddHostedService<SyncServiceWorker>();
                services.AddSyncRepositories();
                services.AddEventHandlers();
            })
            .UseSerilog((context, loggerConfig) =>
            {
                loggerConfig.ReadFrom.Configuration(context.Configuration);
            });
}
