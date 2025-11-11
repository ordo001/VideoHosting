namespace VideoHostingApi.Auth.Services.Contracts.Models;

public class LoginModel
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