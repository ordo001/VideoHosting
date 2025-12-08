using VideoHostingApi.Common.Entities.Video;
using VideoHostingApi.Common.Messaging;
using VideoHostingApi.VideoHandler.Services;
using VideoHostingApi.VideoHandler.Services.Contracts.Models;
using VideoHostringApi.Common.Messaging.Contracts;
using VideoHostringApi.VideoHandler.Context;

namespace VideoHostingApi.VideoHandler.Worker.Extentions;

public static class ConfigureVideoHandler
{
    public static IServiceCollection ConfigureFileService(this IServiceCollection services)
    {
        services.AddScoped<IMessageHandler<VideoProcessingMessage>, FFmpegVideoProcessingHandler>();
        services.AddSingleton<IMessageConsumer<VideoProcessingMessage>, RabbitMqMessageConsumer<VideoProcessingMessage>>();
        
        return services;
    }
}