using AutoMapper;
using VideoHostingApi.Common.Entities.Video;
using VideoHostingApi.Common.Entities.Video.Enums;
using VideoHostingApi.Common.Repositories.Contracts;
using VideoHostingApi.FileService.Repositories.Contracts;
using VideoHostingApi.FileService.Service.Contracts;
using VideoHostingApi.FileService.Service.Contracts.Models;
using VideoHostingApi.FileService.Service.Contracts.Models.Events;
using VideoHostingApi.FileService.Service.Exceptions;
using VideoHostringApi.Common.Messaging.Contracts;
using ObjectNotFoundException = Minio.Exceptions.ObjectNotFoundException;

namespace VideoHostingApi.FileService.Service;

/// <summary>
/// Сервис для работы с видео
/// </summary>
public class VideoService(IObjectStorageRepository<VideoFile> videoObjectStorageRepository,
    IVideoRepository videoRepository, IVideoFileRepository videoFileRepository,
    IMapper mapper, IMessageProducer messageProducer) : IVideoService
{
    public async Task<CreateVideoModel> GetPresignedUploadUrl(UploadVideoModel uploadVideoModel, CancellationToken cancellationToken)
    {
        var video = new Video
        {
            UserId = uploadVideoModel.UserId,
            Title = uploadVideoModel.Title,
            Description = uploadVideoModel.Description,
            Status = Status.PendingUpload,
            IsPublic = uploadVideoModel.IsPublic,
            CreatedAt = DateTime.UtcNow,
        };
        
        videoRepository.Add(video);

        var objectName = $"{video.Id}/master";

        var file = new VideoFile
        {
            VideoId = video.Id,
            Path = objectName,
            Type = "master",
            Quality = null,
            CreatedAt = video.CreatedAt,
        };
        
        videoFileRepository.Add(file);
        await videoFileRepository.SaveChanges(cancellationToken);
        
        await videoObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
        var url = await videoObjectStorageRepository.GetPresignedUploadUrl(objectName);
        
        return new CreateVideoModel
        {
            VideoId = video.Id,
            UploadUrl = url,
            ObjectKey = objectName,
        };
    }

    public async Task UploadCompete(Guid videoId, CancellationToken cancellationToken)
    {
        // var video = await videoRepository.GetById(videoId, cancellationToken);
        // if (video is null)
        // {
        //     throw new FileEntityNotFoundException($"Видео с идентификатором {videoId} не найдено");
        // }
        //
        // video.Status = Status.Uploaded;
        // videoRepository.Update(video);
        // await videoRepository.SaveChanges(cancellationToken);
        
        //await messageProducer.SendMessage("video-processing",video.Id);
        
        await messageProducer.SendMessage("video-processing",new FileUploadedEvent {  VideoId = videoId }, cancellationToken);

    }

    public async Task<string> GetPresignedDownloadUrl(Guid videoId, CancellationToken cancellationToken)
    {
        try
        {
            var video = await CheckVideoById(videoId, cancellationToken);
            
            await videoObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
            var objectName = $"{video.Id}/master";
            
            var url = await videoObjectStorageRepository.GetPresignedDownloadUrl(objectName);
            return url;
        }
        catch (ObjectNotFoundException ex)
        {
            throw new VideoHostingApi.FileService.Service.Exceptions.ObjectNotFoundException($"Видео с идентификатором {videoId} не найден в S3 хранилище");
        }
    }

    public async Task UploadFile(AddFileModel addFileModel, CancellationToken cancellationToken)
    {
        var video = new Video
        {
            UserId = addFileModel.UserId,
            Title = addFileModel.Title,
            Description = addFileModel.Description,
            Status = Status.PendingUpload,
            IsPublic = addFileModel.IsPublic,
            CreatedAt = DateTime.UtcNow,
        };
        videoRepository.Add(video);
        
        var objectName = $"{video.Id}/master";
        
        var file = new VideoFile
        {
            VideoId = video.Id,
            Path = objectName,
            Type = "master",
            Quality = null,
            CreatedAt = video.CreatedAt,
        };
        
        videoFileRepository.Add(file);
        await videoFileRepository.SaveChanges(cancellationToken);
        
        await videoObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
        await videoObjectStorageRepository.UploadFile(file.Path, addFileModel.FileStream, addFileModel.ContentType, cancellationToken);
    }

    public async Task<FileModel> DownloadFile(string name, CancellationToken cancellationToken)
    {
        // try
        // {
        //     await CheckVideoById(name, cancellationToken);
        //     
        //     await videoObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
        //     var model = await videoObjectStorageRepository.DownloadFile(name, cancellationToken);
        //     return new FileModel{ FileStream = model.FileStream, ContentType = model.ContentType, FileName = model.FileName }; // TODO: Заменить на автомаппинг
        // }
        // catch (ObjectNotFoundException ex)
        // {
        //     throw new VideoHostingApi.FileService.Service.Exceptions.ObjectNotFoundException($"Видео с названием {name} не найден в S3 хранилище");
        // }
        throw new NotImplementedException();
    }
    
    public async Task DeleteFile(string name, CancellationToken cancellationToken)
    {
        // await CheckVideoById(name, cancellationToken);
        //
        // try
        // {
        //     await videoObjectStorageRepository.DeleteFile(name, cancellationToken);
        // }
        // catch (ObjectNotFoundException ex)
        // {
        //     throw new VideoHostingApi.FileService.Service.Exceptions.ObjectNotFoundException($"Видео с названием {name} не найден в S3 хранилище");
        // }
        throw new NotImplementedException();
    }
    
    private async Task<Video> CheckVideoById(Guid videoId, CancellationToken cancellationToken)
    {
        var video = await videoRepository.GetById(videoId, cancellationToken);
        if (video is null)
        {
            throw new FileEntityNotFoundException($"Сущность с идентификатором {videoId} не найдена");
        }

        return video;
    }
}