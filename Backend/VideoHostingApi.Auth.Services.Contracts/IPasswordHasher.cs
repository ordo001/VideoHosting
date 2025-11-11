namespace VideoHostingApi.Auth.Services.Contracts;

/// <summary>
/// Интерфейс для генератора хэшей
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Cгенерировать хэш пароля
    /// </summary>
    public string GeneratePasswordHash(string password);
    
    /// <summary>
    /// Проверить хэш пароля
    /// </summary>
    public bool VerifyHash(string password, string hash);
}