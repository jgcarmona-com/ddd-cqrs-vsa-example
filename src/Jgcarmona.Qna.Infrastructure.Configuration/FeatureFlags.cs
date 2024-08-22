namespace Jgcarmona.Qna.Infrastructure.Configuration
{
    public class FeatureFlags
    {
        // RabbitMQ or AzureEventHub
        public string MessagingProvider { get; set; } = "RabbitMQ";
    }
}
