using Jgcarmona.Qna.Domain.Events;

namespace Jgcarmona.Qna.Domain.Abstract
{
    public interface IEventListener
    {
        Task StartListeningAsync(Func<EventBase, Task> onEventReceived, CancellationToken cancellationToken);
    }
}
