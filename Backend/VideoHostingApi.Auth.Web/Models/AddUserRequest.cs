namespace VideoHostingApi.Auth.Web.Models;

/// <summary>
/// Модель запроса на создание пользователя
/// </summary>
public class AddUserRequest
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = string.Empty;
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } =  string.Empty;
    
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; } =  string.Empty;
    
    /// <summary>
    /// Идентификатор роли
    /// </summary>
    public Guid RoleId  { get; set; }
}