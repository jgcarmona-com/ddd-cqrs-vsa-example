using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Jgcarmona.Qna.Infrastructure.Messaging;
using Serilog;
using Jgcarmona.Qna.Infrastructure.Extensions;
using Jgcarmona.Qna.Common.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Jgcarmona.Qna.Services.StatsService;


public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        try
        {
            Log.Information("Starting SyncService");
            var host = CreateHostBuilder(args).Build();

            // Start the messaging listener
            var listener = host.Services.GetRequiredService<IMessagingListener>();
            await listener.StartListeningAsync(async (message) =>
            {
                // Process the incoming message with your business logic
                Log.Information($"SyncService is processing message: {message}");
                await Task.CompletedTask; // Placeholder for actual message processing logic
            }, CancellationToken.None);

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
            .UseSerilog()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                // Load the configuration files
                config.SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                      .AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Register the configuration settings for CommonFeatureFlags
                services.Configure<CommonFeatureFlags>(hostContext.Configuration.GetSection("CommonFeatureFlags"));

                // Register the appropriate messaging listener based on the configured provider
                services.AddMessagingListener(hostContext.Configuration);

                // Register the main hosted service for SyncService
                services.AddHostedService<StatsService>();
            });
}
