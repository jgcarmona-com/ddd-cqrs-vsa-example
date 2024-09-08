using Jgcarmona.Qna.Common.Configuration;
using Jgcarmona.Qna.Domain.Services;
using Jgcarmona.Qna.Infrastructure.Messaging.AzureEventBus;
using Jgcarmona.Qna.Infrastructure.Messaging.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Jgcarmona.Qna.Infrastructure.Extensions
{
    public static class EventDispatcherServiceCollectionExtensions
    {
        public static IServiceCollection AddEventDispatcher(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FeatureFlags>(x => configuration.GetSection("FeatureFlags"));

            var serviceProvider = services.BuildServiceProvider();
            var featureFlags = serviceProvider.GetRequiredService<IOptions<FeatureFlags>>().Value;

            switch (featureFlags.MessagingProvider.ToLower())
            {
                case "rabbitmq":
                    var rabbitMQSettings = new RabbitMQSettings();
                    configuration.GetSection("RabbitMQSettings").Bind(rabbitMQSettings);

                    // Overwrite the values from environment variables:
                    rabbitMQSettings.HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOSTNAME") ?? rabbitMQSettings.HostName;
                    rabbitMQSettings.UserName = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? rabbitMQSettings.UserName;
                    rabbitMQSettings.Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? rabbitMQSettings.Password;
                    rabbitMQSettings.QueueName = Environment.GetEnvironmentVariable("RABBITMQ_QUEUE_NAME") ?? rabbitMQSettings.QueueName;

                    services.Configure<RabbitMQSettings>(options =>
                        {
                            options.HostName = rabbitMQSettings.HostName;
                            options.UserName = rabbitMQSettings.UserName;
                            options.Password = rabbitMQSettings.Password;
                            options.QueueName = rabbitMQSettings.QueueName;
                            options.ExchangeName = rabbitMQSettings.ExchangeName;
                        });

                    services.AddScoped<IEventDispatcher, RabbitMQEventDispatcher>();
                    break;
                case "azureeventhub":
                    services.AddScoped<IEventDispatcher, AzureEventHubDispatcher>();
                    break;
            }

            return services;
        }
    }
}
