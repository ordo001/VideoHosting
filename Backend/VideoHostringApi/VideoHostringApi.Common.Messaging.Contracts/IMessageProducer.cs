namespace VideoHostringApi.Common.Messaging.Contracts;

/// <summary>
/// Интерфейс поставщика сообщений
/// </summary>
public interface IMessageProducer
{
    /// <summary>
    /// Отправить сообщение в очередь
    /// </summary>
    public Task SendMessage<T>(string queue, T message);
}