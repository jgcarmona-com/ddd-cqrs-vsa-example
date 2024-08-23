using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Infrastructure.Messaging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Jgcarmona.Qna.Services.SyncService;

public class SyncServiceWorker : BackgroundService
{
    private readonly IMessagingListener _messagingListener;
    private readonly ILogger<SyncServiceWorker> _logger;

    public SyncServiceWorker(IMessagingListener messagingListener, ILogger<SyncServiceWorker> logger)
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
                    // Here is where the logic to handle the specific domain event goes
                    _logger.LogInformation("SyncService received event: {EventId}, occurred on {OccurredOn}",
                        domainEvent.Id, domainEvent.OccurredOn);
                }
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing the message in SyncService.");
            }
            await Task.CompletedTask;

        }, stoppingToken);
    }
}
