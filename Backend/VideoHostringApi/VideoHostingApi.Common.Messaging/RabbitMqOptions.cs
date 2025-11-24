namespace VideoHostingApi.Common.Messaging;

/// <summary>
/// Настройки RabbitMq
/// </summary>
public class RabbitMqOptions
{
    /// <summary>
    /// Хост
    /// </summary>
    public string Host { get; set; } = string.Empty;
    
    /// <summary>
    /// Порт
    /// </summary>
    public int Port { get; set; } = 5672;
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Username { get; set; } = string.Empty;
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = string.Empty;
    
    /// <summary>
    /// Виртуальный хост
    /// </summary>
    public string VirtualHost { get; set; } = string.Empty;
}