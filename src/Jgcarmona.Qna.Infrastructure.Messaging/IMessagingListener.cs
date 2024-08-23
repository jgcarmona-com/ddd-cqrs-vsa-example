namespace Jgcarmona.Qna.Infrastructure.Messaging
{
    public interface IMessagingListener
    {
        Task StartListeningAsync(Func<string, Task> onMessageReceived, CancellationToken cancellationToken);
    }
}
