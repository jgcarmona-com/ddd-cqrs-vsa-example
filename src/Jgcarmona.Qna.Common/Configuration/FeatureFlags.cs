namespace Jgcarmona.Qna.Common.Configuration.Configuration
{
    public class FeatureFlags
    {
        public bool MigrateAtStartup { get; set; } = false;
        public string MessagingProvider { get; set; } = "RabbitMQ";
    }
}
