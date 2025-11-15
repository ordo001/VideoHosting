namespace VideoHostingApi.FileService.Service.Contracts.Models;

/// <summary>
/// Модель метаднны файла
/// </summary>
public class FileMetadata
{
    /// <summary>
    /// Mime type
    /// </summary>
    public string ContentType { get; set; } = string.Empty;
    
    /// <summary>
    /// Имя файла
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Размер файла
    /// </summary>
    public long Size { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Дата загрузки файла
    /// </summary>
    public DateTime UploadedAt { get; set; }
}