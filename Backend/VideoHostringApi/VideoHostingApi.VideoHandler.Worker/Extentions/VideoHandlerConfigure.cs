using VideoHostingApi.Common.Entities.Video;
using VideoHostingApi.Common.Messaging;
using VideoHostingApi.VideoHandler.Services;
using VideoHostingApi.VideoHandler.Services.Contracts.Models;
using VideoHostringApi.Common.Messaging.Contracts;
using VideoHostringApi.VideoHandler.Context;

namespace VideoHostingApi.VideoHandler.Worker.Extentions;

/// <summary>
/// Конфигурации для обработчика видео
/// </summary>
public static class VideoHandlerConfigure
{
    /// <summary>
    /// Сконфигурировать зависимости обработчика видео
    /// </summary>
    public static IServiceCollection ConfigureVideoHandler(this IServiceCollection services)
    {
        services.AddScoped<IMessageHandler<VideoProcessingMessage>, VideoProcessingHandler>();
        services.AddSingleton<IMessageConsumer<VideoProcessingMessage>, RabbitMqMessageConsumer<VideoProcessingMessage>>();
        
        return services;
    }
}