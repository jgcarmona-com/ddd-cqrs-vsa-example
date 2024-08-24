using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Infrastructure.Messaging;

public class AzureEventHubListener : IMessagingListener
{
    public Task StartListeningAsync(Func<EventBase, Task> onEventReceived, CancellationToken cancellationToken)
    {
        // TODO: Implement the logic to listen to the Azure Event Hub
        throw new NotImplementedException();
    }
}
