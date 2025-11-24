using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using VideoHostingApi.Common.Web;
using VideoHostingApi.FileService.Entities;
using VideoHostingApi.FileService.Repositories;
using VideoHostingApi.FileService.Repositories.Contracts;
using VideoHostingApi.FileService.Service;
using VideoHostingApi.FileService.Service.Contracts;

namespace VideoHostingApi.FileService.Web.Extentions;

/// <summary>
/// Сконфигурировать файловый сервис
/// </summary>
public static class FileServiceConfigure
{
    public static IServiceCollection ConfigureFileService(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.RegisterAssemblyInterfacesAssignableTo<IFileRepositoryAnchor>(ServiceLifetime.Scoped);
        services.AddScoped<IVideoService, VideoService>();

        var minioClient = new MinioClient()
            .WithEndpoint(configuration["Minio:PublicEndpoint"])
            .WithCredentials(configuration["Minio:AccessKey"], configuration["Minio:SecretKey"])
            .Build();

        services.AddSingleton(minioClient);
        
        services.AddScoped<IObjectStorageRepository<VideoFile>>(sp =>
            new MinioRepository<VideoFile>(minioClient, configuration["MinioBuckets:videos"]!));
        

        return services;
    }
}