using VideoHostingApi.VideoHandler.Services.Contracts.Models;

namespace VideoHostingApi.VideoHandler.Services.Contracts;

public interface IVideoProcessingService
{
    public Task ProcessVideoAsync(VideoProcessingMessage videoProcessingMessage);
}