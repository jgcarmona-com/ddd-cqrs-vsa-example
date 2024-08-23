using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Infrastructure.Messaging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

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
        await _messagingListener.StartListeningAsync(async (message) =>
        {
            try
            {
                // Deserialize the message into a domain event
                var domainEvent = JsonSerializer.Deserialize<EventBase>(message);

                if (domainEvent != null)
                {
                    // Log and process the received event
                    _logger.LogInformation("StatsService received event: {EventId}, occurred on {OccurredOn}",
                        domainEvent.Id, domainEvent.OccurredOn);

                    // Add your specific stats handling logic here
                }
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing the message in StatsService.");
            }
            await Task.CompletedTask;

        }, stoppingToken);
    }
}
