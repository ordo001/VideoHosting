namespace VideoHostingApi.Auth.Web.Models;

/// <summary>
/// Модель запроса на вход
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } =  string.Empty;
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } =  string.Empty;
}