namespace VideoHostingApi.Auth.Services.Contracts.Models;

/// <summary>
/// Модель пользователя
/// </summary>
public class UserModel
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = string.Empty;
    
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Хеш пароля
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;
    
    /// <summary>
    /// Идентификатор роли
    /// </summary>
    public Guid RoleId { get; set; }
}