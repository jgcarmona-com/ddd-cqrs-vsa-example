using Jgcarmona.Qna.Domain.Services;

namespace Jgcarmona.Qna.Services.NotificationService
{
    public class NotificationServiceWorker : BackgroundService
    {
        private readonly IEventListener _messagingListener;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NotificationServiceWorker> _logger;

        public NotificationServiceWorker(IEventListener messagingListener, IServiceProvider serviceProvider, ILogger<NotificationServiceWorker> logger)
        {
            _messagingListener = messagingListener;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _messagingListener.StartListeningAsync(async (domainEvent) =>
            {
                try
                {
                    _logger.LogInformation("SyncService received event: {EventId}, occurred on {OccurredOn}", domainEvent.Id, domainEvent.OccurredOn);

                    var eventType = domainEvent.GetType();
                    var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
                    var handler = _serviceProvider.GetService(handlerType);

                    if (handler != null)
                    {
                        var method = handlerType.GetMethod("Handle");
                        await (Task)method.Invoke(handler, new object[] { domainEvent });
                    }
                    else
                    {
                        _logger.LogWarning("No handler found for event type: {EventType}", eventType.Name);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing the event in SyncService.");
                }
            }, stoppingToken);
        }
    }
}
