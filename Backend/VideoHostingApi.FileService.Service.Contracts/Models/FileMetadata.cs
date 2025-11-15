namespace VideoHostingApi.FileService.Service.Contracts.Models;

/// <summary>
/// Модель метаднны файла
/// </summary>
public class FileMetadata
{
    /// <summary>
    /// Потом файла
    /// </summary>
    public Stream FileStream { get; set; } = null!;
    
    /// <summary>
    /// Mime type
    /// </summary>
    public string ContentType { get; set; } = string.Empty;
    
    /// <summary>
    /// Имя файла
    /// </summary>
    public string FileName { get; set; } = string.Empty;
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Дата загрузки файла
    /// </summary>
    public DateTime UploadedAt { get; set; }
}