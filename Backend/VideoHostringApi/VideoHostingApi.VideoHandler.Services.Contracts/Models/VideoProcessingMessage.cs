namespace VideoHostingApi.VideoHandler.Services.Contracts.Models;

/// <summary>
/// Модель сообщения для обработки видео
/// </summary>
public class VideoProcessingMessage
{
    /// <summary>
    /// Идентификатор обрабатываемого видео
    /// </summary>
    public Guid VideoId { get; set; }
}