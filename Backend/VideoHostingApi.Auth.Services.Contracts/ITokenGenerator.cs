using VideoHostingApi.Auth.Services.Contracts.Models;

namespace VideoHostingApi.Auth.Services.Contracts;

/// <summary>
/// Интерфейс для генратора токенов
/// </summary>
public interface ITokenGenerator
{
    /// <summary>
    /// Сгенерировать токен для запроса на вход
    /// </summary>
    public Task<Token> GenerateToken(LoginModel loginRequest, CancellationToken cancellationToken);
}