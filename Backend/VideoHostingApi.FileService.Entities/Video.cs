using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.FileService.Entities;

/// <summary>
/// Модель видео
/// </summary>
public class Video : EntityBase
{
    /// <summary>
    /// Название видео
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Mime type
    /// </summary>
    public string ContentType { get; set; } = string.Empty;
    
    /// <summary>
    /// Размер видео
    /// </summary>
    public long Size { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Дата загрузки
    /// </summary>
    public DateTime UploadedAt { get; set; }
}