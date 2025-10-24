namespace VideoHostingApi.FileService.Repositories.Contracts.Models;

/// <summary>
/// Модель файла из S3 хранилища
/// </summary>
public class FileDbModel
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
}