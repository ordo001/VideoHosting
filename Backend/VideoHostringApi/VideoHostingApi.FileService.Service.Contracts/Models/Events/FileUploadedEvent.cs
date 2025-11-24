namespace VideoHostingApi.FileService.Service.Contracts.Models.Events;

/// <summary>
/// Модель события когда файл загружен
/// </summary>
public class FileUploadedEvent
{
    /// <summary>
    /// Идентификатор файла видео
    /// </summary>
    public Guid FileId { get; set; }
    
    /// <summary>
    /// Путь исходного видео
    /// </summary>
    public string MasterPath { get; set; } = string.Empty;
}