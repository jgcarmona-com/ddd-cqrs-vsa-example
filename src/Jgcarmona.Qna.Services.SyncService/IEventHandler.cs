using Jgcarmona.Qna.Domain.Events;

namespace Jgcarmona.Qna.Services.SyncService
{
    public interface IEventHandler<in TEvent> where TEvent : EventBase
    {
        Task Handle(TEvent domainEvent);
    }

}
