
using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.FileService.Entities;

/// <summary>
/// Модель файла видео
/// </summary>
public class VideoFile : EntityBase
{
    /// <summary>
    /// Идентификатор сущности видео
    /// </summary>
    public Guid VideoId { get; set; }
    
    /// <summary>
    /// Путь к файлу в S3 
    /// </summary>
    public string Path { get; set; } = string.Empty;
    
    /// <summary>
    /// Тип файла
    /// </summary>
    public string Type { get; set; } = string.Empty;
    
    /// <summary>
    /// Качество
    /// </summary>
    public string? Quality { get; set; }
    
    /// <summary>
    /// Размер 
    /// </summary>
    public long Size { get; set; }
    
    /// <summary>
    /// Дата добавления
    /// </summary>
    public DateTime CreatedAt { get; set; }
}