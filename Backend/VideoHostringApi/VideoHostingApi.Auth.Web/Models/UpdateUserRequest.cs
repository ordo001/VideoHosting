namespace VideoHostingApi.Auth.Web.Models;

/// <summary>
/// Модель запроса на обновление пользователя
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// Имя
    /// </summary>
    public string? Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Логин
    /// </summary>
    public string? Login { get; set; } = string.Empty;
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string? Password { get; set; } = string.Empty;
    
    /// <summary>
    /// Идентификатор роли
    /// </summary>
    public Guid RoleId { get; set; }
}