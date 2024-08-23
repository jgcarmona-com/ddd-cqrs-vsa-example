using Jgcarmona.Qna.Common.Configuration;
using Jgcarmona.Qna.Domain.Abstract.Events;
using Jgcarmona.Qna.Infrastructure.EventDispatchers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Jgcarmona.Qna.Api.Common.Extensions
{
    public static class EventDispatcherServiceCollectionExtensions
    {
        public static IServiceCollection AddEventDispatcher(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CommonFeatureFlags>(x => configuration.GetSection("CommonFeatureFlags"));

            var serviceProvider = services.BuildServiceProvider();
            var commonFeatureFlags = serviceProvider.GetRequiredService<IOptions<CommonFeatureFlags>>().Value;

            switch (commonFeatureFlags.MessagingProvider.ToLower())
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

                    services.AddScoped<IEventDispatcher, RabbitMQEventDispatcher>();
                    break;
                case "azureeventhub":
                    services.AddScoped<IEventDispatcher, AzureEventHubDispatcher>();
                    break;
                default:
                    services.AddScoped<IEventDispatcher, InMemoryEventDispatcher>();
                    break;
            }

            return services;
        }
    }
}
