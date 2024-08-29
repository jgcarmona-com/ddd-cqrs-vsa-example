using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Abstract;

namespace Jgcarmona.Qna.Services.StatsService;

public class StatsServiceWorker : BackgroundService
{
    private readonly IEventListener _messagingListener;
    private readonly ILogger<StatsServiceWorker> _logger;

    public StatsServiceWorker(IEventListener messagingListener, ILogger<StatsServiceWorker> logger)
    {
        _messagingListener = messagingListener;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _messagingListener.StartListeningAsync(async (domainEvent) =>
        {
            try
            {
                // Log the received event
                _logger.LogInformation("StatsService received event: {EventId}, occurred on {OccurredOn}",
                    domainEvent.Id, domainEvent.OccurredOn);

                // Add your specific stats handling logic here based on the event type
                if (domainEvent is AccountViewedEvent userViewedEvent)
                {
                    // Handle the AccountViewedEvent specifically
                    _logger.LogInformation("Processing AccountViewedEvent for user {AuthorId}, username: {Name}",
                        userViewedEvent.UserId, userViewedEvent.Username);

                    // Add additional logic to update stats based on the event
                }
                else
                {
                    _logger.LogWarning("Received an unhandled event type: {EventType}", domainEvent.GetType().Name);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing the event in StatsService.");
            }

            await Task.CompletedTask;

        }, stoppingToken);
    }
}
