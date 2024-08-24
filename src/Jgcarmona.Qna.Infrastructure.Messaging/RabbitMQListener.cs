using Jgcarmona.Qna.Common.Configuration;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Infrastructure.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog.Context;
using System.Text;
using System.Text.Json;

public class RabbitMQListener : IMessagingListener
{
    private readonly RabbitMQSettings _settings;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<RabbitMQListener> _logger;

    public RabbitMQListener(IOptions<RabbitMQSettings> settings, ILogger<RabbitMQListener> logger)
    {
        _settings = settings.Value;
        _logger = logger;

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

    public Task StartListeningAsync(Func<EventBase, Task> onEventReceived, CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            // Extract the CorrelationId from the headers
            string correlationId = ea.BasicProperties.Headers.ContainsKey("CorrelationId")
                // Ensure the byte[] is converted to string
                ? Encoding.UTF8.GetString((byte[])ea.BasicProperties.Headers["CorrelationId"]) 
                : Guid.NewGuid().ToString(); // Generate a new CorrelationId if not present

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var messagePayload = JsonSerializer.Deserialize<JsonElement>(message);
                    var eventType = messagePayload.GetProperty("EventType").GetString();
                    var eventData = messagePayload.GetProperty("EventData").GetRawText();

                    var type = Type.GetType(eventType);
                    var domainEvent = (EventBase)JsonSerializer.Deserialize(eventData, type);

                    if (domainEvent != null)
                    {
                        domainEvent.CorrelationId = correlationId; // Ensure the CorrelationId is set
                        await onEventReceived(domainEvent);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message in RabbitMQListener.");
                }
            }
        };

        _channel.BasicConsume(queue: _settings.QueueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }
}
