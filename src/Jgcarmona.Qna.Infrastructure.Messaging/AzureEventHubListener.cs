using Qna.Infrastructure.Messaging;

public class AzureEventHubListener : IMessagingListener
{
    public Task StartListeningAsync(CancellationToken cancellationToken = default)
    {
        // TODO: Implement the logic to listen to the Azure Event Hub
        return Task.CompletedTask;
    }
}
