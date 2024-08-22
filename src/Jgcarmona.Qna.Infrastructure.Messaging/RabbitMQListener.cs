using Qna.Infrastructure.Messaging;

public class RabbitMQListener : IMessagingListener
{
    public Task StartListeningAsync(CancellationToken cancellationToken = default)
    {
        // TODO: Implement the logic to listen to the RabbitMQ
        return Task.CompletedTask;
    }
}
