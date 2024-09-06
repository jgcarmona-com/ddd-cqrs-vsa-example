using DotNetEnv;
using Jgcarmona.Qna.Common.Configuration;
using Jgcarmona.Qna.Infrastructure.Extensions;
using Jgcarmona.Qna.Services.Common;
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
                    services.Configure<SmtpSettings>(hostContext.Configuration.GetSection("SmtpSettings"));
                    services.Configure<ApiSettings>(hostContext.Configuration.GetSection("ApiSettings"));  

                    var smtpSettings = hostContext.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
                    if (string.IsNullOrEmpty(smtpSettings?.User) || string.IsNullOrEmpty(smtpSettings.Password))
                    {
                        throw new InvalidOperationException("SMTP user and password are required.");
                    }
                    services
                    .AddFluentEmail(smtpSettings.SenderEmail, smtpSettings.SenderName)
                    .AddSmtpSender(smtpSettings.Host, smtpSettings.Port, smtpSettings.User, smtpSettings.Password);

                    services.AddMessagingListener(hostContext.Configuration);
                    services.AddHostedService<NotificationServiceWorker>();
                    services.AddEventHandlers();
                })
                .UseSerilog((context, loggerConfig) =>
                {
                    loggerConfig.ReadFrom.Configuration(context.Configuration);
                });
    }
}
