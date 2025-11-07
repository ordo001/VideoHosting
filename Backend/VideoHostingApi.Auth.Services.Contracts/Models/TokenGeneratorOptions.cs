namespace VideoHostingApi.Auth.Services.Contracts.Models;

/// <summary>
/// Настройки генерации токена
/// </summary>
public class TokenGeneratorOptions
{
    /// <summary>
    /// Ключ для создания токена
    /// </summary>
    public string Secret { get; init; } = string.Empty;

    /// <summary>
    /// Время жизни токена в секундах
    /// </summary>
    public int ExpirationSeconds { get; init; }
}