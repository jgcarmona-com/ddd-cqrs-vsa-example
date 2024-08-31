using Jgcarmona.Qna.Common.Configuration.Configuration;
using Jgcarmona.Qna.Common.Converters;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Services;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Jgcarmona.Qna.Infrastructure.EventDispatchers
{
    public class RabbitMQEventDispatcher : IEventDispatcher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly RabbitMQSettings _settings;

        public RabbitMQEventDispatcher(IOptions<RabbitMQSettings> settings)
        {
            _settings = settings.Value;
            var factory = new ConnectionFactory()
            {
                HostName = _settings.HostName,
                UserName = _settings.UserName,
                Password = _settings.Password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Declare the exchange
            _channel.ExchangeDeclare(
                exchange: _settings.ExchangeName,
                type: "fanout",  // Use "fanout" to broadcast the message to multiple queues
                durable: true,
                autoDelete: false,
                arguments: null
            );

            // NOTE: bind queues to the exchange in the consumer side
        }

        public Task DispatchAsync<TEvent>(TEvent domainEvent) where TEvent : EventBase
        {
            // Include the assembly name to deserialize the event
            var eventType = domainEvent.GetType().AssemblyQualifiedName;
            var messagePayload = new
            {
                EventType = eventType,
                EventData = domainEvent
            };
            var jsonOptions = new JsonSerializerOptions
            {
                Converters = { new UlidJsonConverter() }
            };
            var message = JsonSerializer.Serialize(messagePayload, jsonOptions);

            var body = Encoding.UTF8.GetBytes(message);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.Headers = new Dictionary<string, object>
            {
                { "CorrelationId", domainEvent.CorrelationId }
            };

            _channel.BasicPublish(exchange: _settings.ExchangeName, routingKey: "", basicProperties: properties, body: body);
            return Task.CompletedTask;
        }

    }
}
