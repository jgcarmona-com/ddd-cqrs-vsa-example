using DotNetEnv;
using Jgcarmona.Qna.Infrastructure.Extensions;
using Serilog;

namespace Jgcarmona.Qna.Services.NotificationService
{
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
                Log.Information("Starting NotificationService");
                var host = CreateHostBuilder(args).Build();

                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "NotificationService terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, loggerConfig) =>
                {
                    loggerConfig.ReadFrom.Configuration(context.Configuration);
                })
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

                    // TODO: set userr and password for email (SMTP rpovider)
                    services
                    .AddFluentEmail(hostContext.Configuration["Email:SenderEmail"], hostContext.Configuration["Email:SenderName"])
                    .AddSmtpSender(hostContext.Configuration["Email:Host"], hostContext.Configuration.GetValue<int>("Email:Port"));

                    services.AddMessagingListener(hostContext.Configuration);
                    services.AddHostedService<NotificationServiceWorker>();
                    services.AddEventHandlers();
                });
    }
}
