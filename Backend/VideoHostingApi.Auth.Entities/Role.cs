using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.Auth.Entities;

/// <summary>
/// Роль пользователя
/// </summary>
public class Role : EntityBase
{ 
    /// <summary>
    /// Название роли
    /// </summary>
    public string Name { get; set; } = string.Empty;
}