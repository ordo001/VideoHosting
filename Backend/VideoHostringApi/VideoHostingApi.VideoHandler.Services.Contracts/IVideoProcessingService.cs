using VideoHostingApi.VideoHandler.Services.Contracts.Models;

namespace VideoHostingApi.VideoHandler.Services.Contracts;

/// <summary>
/// Интерфейс сервиса обработки видео
/// </summary>
public interface IVideoProcessingService
{
    /// <summary>
    /// Обработать видео
    /// </summary>
    public Task ProcessVideoAsync(Guid videoId, CancellationToken cancellationToken);
}