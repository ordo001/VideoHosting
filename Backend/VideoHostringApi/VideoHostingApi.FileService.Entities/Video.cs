using VideoHostingApi.Common.Entities;
using VideoHostingApi.FileService.Entities.Enums;

namespace VideoHostingApi.FileService.Entities;

/// <summary>
/// Сущность видео
/// </summary>
public class Video : EntityBase
{
    /// <summary>
    /// Заголовок
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <inheritdoc cref="Status"/>
    public Status  Status { get; set; }
    
    /// <summary>
    /// Длительность
    /// </summary>
    public int Duration { get; set; }
    
    /// <summary>
    /// Публичное ли видео
    /// </summary>
    public bool IsPublic { get; set; }

    /// <summary>
    /// Url обложки видео
    /// </summary>
    public string PreviewUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Url главного видео
    /// </summary>
    public string MasterUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Дата добавления
    /// </summary>
    public DateTime CreatedAt { get; set; }
}