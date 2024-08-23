using Jgcarmona.Qna.Common.Configuration;
using Jgcarmona.Qna.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

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
            .UseSerilog()
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
                // Register configuration settings
                services.Configure<CommonFeatureFlags>(hostContext.Configuration.GetSection("CommonFeatureFlags"));

                // Register the appropriate messaging listener based on the configured provider
                services.AddMessagingListener(hostContext.Configuration);

                // Register the main hosted service
                services.AddHostedService<StatsServiceWorker>();
            });
}
