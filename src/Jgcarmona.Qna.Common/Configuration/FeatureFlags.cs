namespace Jgcarmona.Qna.Common.Configuration
{
    public class FeatureFlags
    {
        public bool MigrateAtStartup { get; set; } = false;
        public string MessagingProvider { get; set; } = "RabbitMQ";
        public string SecretsProvider { get; set; } = "AzureKeyVault";

    }
}
