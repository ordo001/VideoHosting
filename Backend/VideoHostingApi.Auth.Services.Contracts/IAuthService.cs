using VideoHostingApi.Auth.Services.Contracts.Models;

namespace VideoHostingApi.Auth.Services.Contracts;

/// <summary>
/// Интерфейс сервиса аутентификации
/// </summary>
public interface IAuthService
{
    public Task<string> LoginAsync(LoginModel loginModel, CancellationToken cancellationToken);
}