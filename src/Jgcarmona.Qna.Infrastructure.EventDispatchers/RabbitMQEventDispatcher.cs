using Jgcarmona.Qna.Domain.Abstract.Events;
using Jgcarmona.Qna.Domain.Events;
using Microsoft.Azure.Amqp;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Jgcarmona.Qna.Infrastructure.EventDispatchers
{
    public class RabbitMQEventDispatcher : IEventDispatcher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQEventDispatcher()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "events_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public Task DispatchAsync(EventBase eventToDispatch)
        {
            var message = JsonSerializer.Serialize(eventToDispatch);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "", routingKey: "events_queue", basicProperties: null, body: body);
            return Task.CompletedTask;
        }
    }
}
