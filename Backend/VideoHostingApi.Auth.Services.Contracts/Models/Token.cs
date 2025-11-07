namespace VideoHostingApi.Auth.Services.Contracts.Models;

/// <summary>
/// Модель JWT токена
/// </summary>
public class Token()
{
    /// <summary>
    /// Значение токена
    /// </summary>
    public string Value { get; set; } = string.Empty;
}
