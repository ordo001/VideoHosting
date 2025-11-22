using VideoHostingApi.Auth.Entities;

namespace VideoHostingApi.Auth.Web.Models;

/// <summary>
/// Модель пользователя для ответа
/// </summary>
public class UserResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public string Login { get; set; } = string.Empty;
    
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Роль
    /// </summary>
    public Role? Role { get; set; } 
}