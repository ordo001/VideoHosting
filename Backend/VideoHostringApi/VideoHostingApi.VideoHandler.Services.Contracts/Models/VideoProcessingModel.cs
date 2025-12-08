namespace VideoHostingApi.VideoHandler.Services.Contracts.Models;

/// <summary>
/// Модель видео на обработку
/// </summary>
public class VideoProcessingModel
{
    /// <summary>
    /// Идентификатор обрабатываемого видео
    /// </summary>
    public Guid VideoId { get; set; }
}