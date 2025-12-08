using VideoHostingApi.VideoHandler.Services.Contracts;
using VideoHostingApi.VideoHandler.Services.Contracts.Models;

namespace VideoHostingApi.VideoHandler.Services;

/// <summary>
/// Сервис по обработке видео
/// </summary>
public class VideoProcessingService : IVideoProcessingService
{
    public Task ProcessVideoAsync(VideoProcessingModel videoProcessingModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}