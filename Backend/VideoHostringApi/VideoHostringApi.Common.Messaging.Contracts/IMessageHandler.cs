namespace VideoHostringApi.Common.Messaging.Contracts;

/// <summary>
/// Интерфейс обработчика сообщений
/// </summary>
public interface IMessageHandler<T>
{
    public Task HandleAsync(T message);
}