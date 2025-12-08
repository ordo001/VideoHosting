using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using VideoHostringApi.Common.Messaging.Contracts;

namespace VideoHostingApi.Common.Messaging;

/// <summary>
/// RabbitMq поставщик
/// </summary>
public class RabbitMqMessageProducer(IChannel channel) : IMessageProducer
{
    public async Task SendMessage<T>(string queue, T message, CancellationToken cancellationToken)
    {
        await channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false, cancellationToken: cancellationToken);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        //var props = channel.CreateBasicProperties();
        //props.Persistent = true;

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: queue,
            body: body,
            cancellationToken
        );
    }
}