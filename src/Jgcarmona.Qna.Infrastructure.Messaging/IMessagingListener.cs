using Jgcarmona.Qna.Domain.Events;

namespace Jgcarmona.Qna.Infrastructure.Messaging
{
    public interface IMessagingListener
    {
        Task StartListeningAsync(Func<EventBase, Task> onEventReceived, CancellationToken cancellationToken);
    }
}
