namespace VideoHostingApi.FileService.Service.Contracts.Models;

/// <summary>
/// Модель файла
/// </summary>
public class FileModel
{
    /// <summary>
    /// Поток файла
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