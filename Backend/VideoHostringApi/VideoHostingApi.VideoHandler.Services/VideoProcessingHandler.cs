using AutoMapper;
using VideoHostingApi.VideoHandler.Services.Contracts;
using VideoHostingApi.VideoHandler.Services.Contracts.Models;
using VideoHostringApi.Common.Messaging.Contracts;
using VideoHostringApi.VideoHandler.Context;

namespace VideoHostingApi.VideoHandler.Services;

/// <summary>
/// Обработчик видео на основе FFmpeg
/// </summary>
public class VideoProcessingHandler(VideoHandlerContext context,
    IMapper mapper, IVideoProcessingService videoProcessingService) : IMessageHandler<VideoProcessingMessage>
{
    public async Task HandleAsync(VideoProcessingMessage videoProcessingMessage)
    {
        try
        {
            Console.WriteLine("Обработка типа да " + videoProcessingMessage.VideoId);
            var model = mapper.Map<VideoProcessingModel>(videoProcessingMessage);

            // TODO: Создать FFmpeg сервис для обработки видео
            await videoProcessingService.ProcessVideoAsync(model.VideoId, CancellationToken.None);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка " + ex.Message);
        }

    }
}