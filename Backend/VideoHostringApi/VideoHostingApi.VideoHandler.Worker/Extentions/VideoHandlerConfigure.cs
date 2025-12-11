using Minio;
using VideoHostingApi.Common.Entities.Video;
using VideoHostingApi.Common.Messaging;
using VideoHostingApi.Common.Repositories;
using VideoHostingApi.Common.Repositories.Contracts;
using VideoHostingApi.VideoHandler.Services;
using VideoHostingApi.VideoHandler.Services.Contracts;
using VideoHostingApi.VideoHandler.Services.Contracts.Models;
using VideoHostringApi.Common.Messaging.Contracts;

namespace VideoHostingApi.VideoHandler.Worker.Extentions;

/// <summary>
/// Конфигурации для обработчика видео
/// </summary>
public static class VideoHandlerConfigure
{
    /// <summary>
    /// Сконфигурировать зависимости обработчика видео
    /// </summary>
    public static IServiceCollection ConfigureVideoHandler(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddScoped<IMessageHandler<VideoProcessingMessage>, VideoProcessingHandler>();
        services.AddScoped<IVideoProcessingService, VideoProcessingService>();
        services.AddScoped<IVideoRepository, VideoRepository>();
        services.AddScoped<IVideoFileRepository, VideoFileRepository>();
        services.AddSingleton<IMessageConsumer<VideoProcessingMessage>, RabbitMqMessageConsumer<VideoProcessingMessage>>();
        
        var minioClient = new MinioClient()
            .WithEndpoint(configuration["Minio:PublicEndpoint"])
            .WithCredentials(configuration["Minio:AccessKey"], configuration["Minio:SecretKey"])
            .Build();

        services.AddSingleton(minioClient);
        
        services.AddScoped<IObjectStorageRepository<VideoFile>>(sp =>
            new MinioRepository<VideoFile>(minioClient, configuration["MinioBuckets:videos"]!));
        
        return services;
    }

    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}