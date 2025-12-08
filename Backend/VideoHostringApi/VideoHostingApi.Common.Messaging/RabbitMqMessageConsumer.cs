using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VideoHostringApi.Common.Messaging.Contracts;

namespace VideoHostingApi.Common.Messaging;

/// <summary>
/// Слушатель сообщений RabbitMq
/// </summary>
/// <param name="channel"></param>
/// <typeparam name="T"></typeparam>
public class RabbitMqMessageConsumer<T>(IChannel channel, IServiceProvider services) : IMessageConsumer<T>
{
    public async Task StartConsuming(string queue)
    { 
        await channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false);
        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (senger, args) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                Console.WriteLine("Получено: " + json);

                var message = JsonSerializer.Deserialize<T>(json);

                if (message is null)
                {
                    Console.WriteLine("Ошибка десериализации ");
                    await channel.BasicAckAsync(args.DeliveryTag, multiple: false);
                    return;
                }

                await using var scope = services.CreateAsyncScope();
                var hendler = scope.ServiceProvider.GetRequiredService<IMessageHandler<T>>();

                await hendler.HandleAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка обработки: " + ex.Message);
                throw;
            }
        };
        
        await channel.BasicConsumeAsync(
            queue: queue,
            autoAck: false,
            consumer: consumer);
    }
}