namespace VideoHostingApi.Auth.Repositories.Contracts.Models;

/// <summary>
/// Модель роли из бд
/// </summary>
public class RoleDbModel
{
     /// <summary>
     /// Идентификатор роли
     /// </summary>
     public Guid Id  { get; set; }
     
     /// <summary>
     /// Название роли
     /// </summary>
     public string RoleName { get; set; } = string.Empty;
}