namespace VideoHostingApi.Auth.Services.Contracts.Models;

/// <summary>
/// Модель авторизации
/// </summary>
public class LoginModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id  { get; set; }
    
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } =  string.Empty;
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } =  string.Empty;
    
    /// <summary>
    /// Роль
    /// </summary>
    public string RoleName = string.Empty;
    
}