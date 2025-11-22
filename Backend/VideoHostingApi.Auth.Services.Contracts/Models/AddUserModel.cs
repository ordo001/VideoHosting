namespace VideoHostingApi.Auth.Services.Contracts.Models;

/// <summary>
/// Модель пользователя на добавление
/// </summary>
public class AddUserModel
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; } =  string.Empty;
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = string.Empty;
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Идентификатор роли
    /// </summary>
    public Guid RoleId { get; set; }
}