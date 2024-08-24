using Jgcarmona.Qna.Common.Configuration;
using Jgcarmona.Qna.Domain.Abstract.Events;
using Jgcarmona.Qna.Domain.Events;
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

            // Declare the queue and bind it to the exchange
            _channel.QueueDeclare(queue: _settings.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: _settings.QueueName, exchange: _settings.ExchangeName, routingKey: "");
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

            var message = JsonSerializer.Serialize(messagePayload);
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
