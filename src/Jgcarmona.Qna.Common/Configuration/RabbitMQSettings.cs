namespace Jgcarmona.Qna.Common.Configuration.Configuration
{
    public class RabbitMQSettings
    {
        public string HostName { get; set; } = "localhost";
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string QueueName { get; set; } = "events_queue";
        public string ExchangeName { get; set; } = "";
        public string RoutingKey { get; set; } = "events_queue";
    }
}
