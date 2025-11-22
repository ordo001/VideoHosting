namespace VideoHostingApi.Auth.Services.Contracts.Models;

/// <summary>
/// Модель для создания токена
/// </summary>
public class GenerateTokenModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = string.Empty;
    
    /// <summary>
    /// Роль
    /// </summary>
    public string RoleName { get; set; } = string.Empty;
}