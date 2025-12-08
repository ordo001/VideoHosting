namespace VideoHostringApi.Common.Messaging.Contracts;

/// <summary>
/// Интерфейс принимающего сообщений
/// </summary>
public interface IMessageConsumer<T>
{
    /// <summary>
    /// Начать прослушивание сообщений в очереди
    /// </summary>
    public Task StartConsuming(string queue, CancellationToken cancellationToken);
}