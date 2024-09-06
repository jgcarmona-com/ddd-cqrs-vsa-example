using Jgcarmona.Qna.Domain.Services;
using Jgcarmona.Qna.Services.Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using Jgcarmona.Qna.Common.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jgcarmona.Qna.Services.Common
{
    public abstract class EventProcessingBackgroundService<TWorker> : BackgroundService
        where TWorker : class
    {
        private readonly IEventListener _messagingListener;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TWorker> _logger;
        private readonly FeatureFlags _featureFlags;

        protected EventProcessingBackgroundService(
            IEventListener messagingListener,
            IServiceProvider serviceProvider,
            ILogger<TWorker> logger,
            IOptions<FeatureFlags> featureFlags)
        {
            _messagingListener = messagingListener;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _featureFlags = featureFlags.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _messagingListener.StartListeningAsync(async (domainEvent) =>
            {
                try
                {
                    _logger.LogInformation("{ServiceName} received event: {EventId}, occurred on {OccurredOn}",
                        typeof(TWorker).Name, domainEvent.Id, domainEvent.OccurredOn);

                    var eventType = domainEvent.GetType();
                    var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
                   
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var handler = scope.ServiceProvider.GetService(handlerType);

                        if (handler != null)
                        {
                            var method = handlerType.GetMethod("Handle");
                            if (method != null)
                            {
                                var task = method.Invoke(handler, new object[] { domainEvent }) as Task;

                                if (task != null)
                                {
                                    await task;
                                }
                                else
                                {
                                    _logger.LogWarning("The 'Handle' method did not return a Task as expected.");
                                }
                            }
                            else
                            {
                                _logger.LogWarning("No 'Handle' method found for handler of event type: {EventType}", eventType.Name);
                            }
                        }
                        else if (_featureFlags.LogUnhandledEvents)
                        {
                            _logger.LogInformation("No handler registered for event type: {EventType}", eventType.Name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing the event in {ServiceName}", typeof(TWorker).Name);
                }
            }, stoppingToken);
        }
    }
}
