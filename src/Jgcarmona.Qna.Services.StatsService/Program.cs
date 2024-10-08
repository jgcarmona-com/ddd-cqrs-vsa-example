using DotNetEnv;
using Jgcarmona.Qna.Common.Configuration;
using Jgcarmona.Qna.Infrastructure.Extensions;
using Jgcarmona.Qna.Services.Common;
using Serilog;

namespace Jgcarmona.Qna.Services.StatsService;

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
            Log.Information("Starting StatsService");
            var host = CreateHostBuilder(args).Build();

            // Start the application
            await host.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "StatsService terminated unexpectedly");
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
                // Load configuration files
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

                // Register the appropriate messaging listener based on the configured provider
                services.AddMessagingListener(hostContext.Configuration);

                // Register the main hosted service

                services.AddEventHandlers();
                services.AddHostedService<StatsServiceWorker>();
            })
            .UseSerilog((context, loggerConfig) =>
            {
                loggerConfig.ReadFrom.Configuration(context.Configuration);
            });
}
