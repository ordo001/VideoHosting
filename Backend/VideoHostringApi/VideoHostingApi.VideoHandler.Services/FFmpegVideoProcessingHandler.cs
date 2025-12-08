using VideoHostingApi.VideoHandler.Services.Contracts;
using VideoHostingApi.VideoHandler.Services.Contracts.Models;
using VideoHostringApi.Common.Messaging.Contracts;
using VideoHostringApi.VideoHandler.Context;

namespace VideoHostingApi.VideoHandler.Services;

/// <summary>
/// Обработчик видео на основе FFmpeg
/// </summary>
public class FFmpegVideoProcessingHandler(VideoHandlerContext context) : IMessageHandler<VideoProcessingMessage>
{
    public Task HandleAsync(VideoProcessingMessage videoProcessingMessage)
    {
        Console.WriteLine("Обработка типа да " + videoProcessingMessage.VideoId);
        return Task.CompletedTask;
    }
}