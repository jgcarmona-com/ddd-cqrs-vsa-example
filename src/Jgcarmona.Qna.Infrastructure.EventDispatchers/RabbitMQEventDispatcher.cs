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
            _channel.QueueDeclare(queue: _settings.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public Task DispatchAsync<TEvent>(TEvent domainEvent) where TEvent : EventBase
        {
            var message = JsonSerializer.Serialize(domainEvent);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(exchange: _settings.ExchangeName, routingKey: _settings.RoutingKey, basicProperties: properties, body: body);
            return Task.CompletedTask;
        }
    }
}
