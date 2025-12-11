namespace VideoHostingApi.FileService.Service.Contracts.Models;

/// <summary>
/// Модель HLS файла
/// </summary>
public class HlsModel
{
    /// <summary>
    /// Поток
    /// </summary>
    public Stream? FileStream { get; set; }
    
    /// <summary>
    /// Тип контента
    /// </summary>
    public string ContentType { get; set; } = string.Empty;
}