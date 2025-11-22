using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.Auth.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User : EntityBase
{
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

    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public Role Role { get; set; } = null!;

}