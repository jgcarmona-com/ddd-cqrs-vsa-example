using Jgcarmona.Qna.Domain.Events;

namespace Jgcarmona.Qna.Services.Common;

public interface IEventHandler<in TEvent> where TEvent : EventBase
{
    Task Handle(TEvent domainEvent);
}
