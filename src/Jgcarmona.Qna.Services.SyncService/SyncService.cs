using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Infrastructure.Messaging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Jgcarmona.Qna.Services.SyncService
{
    public class SyncService : BackgroundService
    {
        private readonly IMessagingListener _messagingListener;
        private readonly ILogger<SyncService> _logger;

        public SyncService(IMessagingListener messagingListener, ILogger<SyncService> logger)
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
                    // Deserializar el mensaje al evento de dominio
                    var domainEvent = JsonSerializer.Deserialize<EventBase>(message);

                    if (domainEvent != null)
                    {
                        // Aquí iría la lógica para manejar el evento de dominio específico
                        _logger.LogInformation("SyncService recibió el evento: {EventId}, ocurrido el {OccurredOn}",
                            domainEvent.Id, domainEvent.OccurredOn);
                    }
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "Error al deserializar el mensaje en SyncService.");
                }
                await Task.CompletedTask;

            }, stoppingToken);
        }
    }
}
