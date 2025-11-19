using VideoHostingApi.Auth.Entities;

namespace VideoHostingApi.Auth.Repositories.Contracts.Models;

/// <summary>
/// Модель пользователя из базы данных
/// </summary>
public class UserDbModel
{
    /// <summary>
    /// Идентфикатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = string.Empty;
    
    /// <summary>
    /// Хэш пароля
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;
    
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Роль
    /// </summary>
    public Role? Role { get; set; }
}