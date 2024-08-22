using Jgcarmona.Qna.Domain.Events;

namespace Jgcarmona.Qna.Domain.Abstract.Events
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<TEvent>(TEvent domainEvent) where TEvent : EventBase;
    }
}
