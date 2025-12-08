namespace VideoHostringApi.Common.Messaging.Contracts;

/// <summary>
/// Интерфейс обработчика сообщений
/// </summary>
public interface IMessageHandler<T>
{
    /// <summary>
    /// Обработать сообщение
    /// </summary>
    public Task HandleAsync(T message);
}