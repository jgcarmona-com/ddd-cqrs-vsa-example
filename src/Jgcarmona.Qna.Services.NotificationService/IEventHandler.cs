using Jgcarmona.Qna.Domain.Events;

namespace Jgcarmona.Qna.Services.NotificationService
{
    public interface IEventHandler<in TEvent> where TEvent : EventBase
    {
        Task Handle(TEvent domainEvent);
    }
}
