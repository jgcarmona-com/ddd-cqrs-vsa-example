using Jgcarmona.Qna.Domain.Events;

namespace Jgcarmona.Qna.Infrastructure.Messaging
{
    public interface IEventListener
    {
        Task StartListeningAsync(Func<EventBase, Task> onEventReceived, CancellationToken cancellationToken);
    }
}
