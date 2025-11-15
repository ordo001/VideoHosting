namespace VideoHostingApi.FileService.Service.Contracts.Models;

/// <summary>
/// Модель файла на добавление
/// </summary>
public class AddFileModel
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
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Размер файла
    /// </summary>
    public long Size { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
}