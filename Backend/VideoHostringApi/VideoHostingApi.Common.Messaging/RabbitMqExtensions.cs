using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using VideoHostringApi.Common.Messaging.Contracts;

namespace VideoHostingApi.Common.Messaging;

/// <summary>
/// Методы расширения для настройки RabbitMq
/// </summary>
public static class RabbitMqExtensions
{
    public static async Task<IServiceCollection> AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection("RabbitMq").Get<RabbitMqOptions>();
        services.AddSingleton(options!);

        var factory = new ConnectionFactory
        {
            HostName = options!.Host,
            Port = options.Port,
            UserName = options.Username,
            Password = options.Password,
            VirtualHost = options.VirtualHost
        };

        services.AddSingleton<IConnectionFactory>(factory);

        var connection = await factory.CreateConnectionAsync();

         services.AddSingleton(connection);

         var channel = await connection.CreateChannelAsync();

        services.AddSingleton(channel);

        services.AddSingleton<IMessageProducer, RabbitMqMessageProducer>();

        return services;
    }
}