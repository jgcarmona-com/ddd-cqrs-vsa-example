using Jgcarmona.Qna.Domain.Abstract.Events;
using Jgcarmona.Qna.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Jgcarmona.Qna.Infrastructure.EventDispatchers
{
    public class InMemoryEventDispatcher : IEventDispatcher
    {
        private readonly ILogger<InMemoryEventDispatcher> _logger;

        public InMemoryEventDispatcher(ILogger<InMemoryEventDispatcher> logger)
        {
            _logger = logger;
        }

        public async Task DispatchAsync<TEvent>(TEvent domainEvent) where TEvent : EventBase
        {
            _logger.LogInformation("Dispatching event {EventName} with CorrelationId: {CorrelationId}", typeof(TEvent).Name, domainEvent.CorrelationId);
            // Lógica de manejo del evento
            await Task.CompletedTask;
        }
    }
}
