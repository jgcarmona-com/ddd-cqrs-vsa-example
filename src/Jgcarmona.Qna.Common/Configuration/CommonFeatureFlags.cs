﻿namespace Jgcarmona.Qna.Common.Configuration.Configuration
{
    public class CommonFeatureFlags
    {
        // RabbitMQ or AzureEventHub
        public string MessagingProvider { get; set; } = "RabbitMQ";
    }
}