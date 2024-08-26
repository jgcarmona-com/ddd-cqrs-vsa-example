using Jgcarmona.Qna.Domain.Abstract;

namespace Jgcarmona.Qna.Services.SyncService;
public class SyncServiceWorker : BackgroundService
{
    private readonly IEventListener _messagingListener;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SyncServiceWorker> _logger;

    public SyncServiceWorker(IEventListener messagingListener, IServiceProvider serviceProvider, ILogger<SyncServiceWorker> logger)
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
