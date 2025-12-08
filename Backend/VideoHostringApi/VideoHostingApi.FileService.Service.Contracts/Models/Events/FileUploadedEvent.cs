namespace VideoHostingApi.FileService.Service.Contracts.Models.Events;

/// <summary>
/// Модель события когда файл загружен
/// </summary>
public class FileUploadedEvent
{
    /// <summary>
    /// Идентификатор видео
    /// </summary>
    public Guid VideoId { get; set; }
}