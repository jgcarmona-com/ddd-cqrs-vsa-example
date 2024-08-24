using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Infrastructure.Messaging;

namespace Jgcarmona.Qna.Services.StatsService;

public class StatsServiceWorker : BackgroundService
{
    private readonly IMessagingListener _messagingListener;
    private readonly ILogger<StatsServiceWorker> _logger;

    public StatsServiceWorker(IMessagingListener messagingListener, ILogger<StatsServiceWorker> logger)
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
                if (domainEvent is UserViewedEvent userViewedEvent)
                {
                    // Handle the UserViewedEvent specifically
                    _logger.LogInformation("Processing UserViewedEvent for user {UserId}, username: {Username}",
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
