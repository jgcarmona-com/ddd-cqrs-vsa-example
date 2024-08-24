using Jgcarmona.Qna.Common.Configuration.Configuration;
using Jgcarmona.Qna.Domain.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jgcarmona.Qna.Infrastructure.Extensions;

public static class MessagingServiceCollectionExtensions
{
    public static IServiceCollection AddMessagingListener(this IServiceCollection services, IConfiguration configuration)
    {
        var featureFlags = configuration.GetSection("CommonFeatureFlags").Get<CommonFeatureFlags>();

        switch (featureFlags.MessagingProvider.ToLower())
        {
            case "rabbitmq":
                var rabbitMQSettings = new RabbitMQSettings();
                configuration.GetSection("RabbitMQSettings").Bind(rabbitMQSettings);
                services.Configure<RabbitMQSettings>(options =>
                    {
                        options.HostName = rabbitMQSettings.HostName;
                        options.UserName = rabbitMQSettings.UserName;
                        options.Password = rabbitMQSettings.Password;
                        options.QueueName = rabbitMQSettings.QueueName;
                        options.ExchangeName = rabbitMQSettings.ExchangeName;
                    });
                services.AddSingleton<IEventListener, RabbitMQEventListener>();
                break;
            case "azureeventhub":
                // Configuración para Azure Event Hub
                break;
            default:
                throw new InvalidOperationException("Invalid messaging provider configured.");
        }

        return services;
    }
}
