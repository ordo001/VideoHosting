using System.Security.Claims;
using AutoMapper;
using Microsoft.Extensions.Logging;
using VideoHostingApi.FileService.Entities;
using VideoHostingApi.FileService.Entities.Enums;
using VideoHostingApi.FileService.Repositories.Contracts;
using VideoHostingApi.FileService.Service.Contracts;
using VideoHostingApi.FileService.Service.Contracts.Models;
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
    public async Task<CreateVideoModel> GetPresignedUploadUrl(Guid userId, CancellationToken cancellationToken)
    { 
        // TODO: Сделать отдельный эндпоинт для проверки подтверждения, что клиент загрузил файл

        var video = new Video
        {
            UserId = userId,
            Status = Status.PendingUpload,
            CreatedAt = DateTime.UtcNow,
        };
        
        videoRepository.Add(video);

        var objectName = $"raw/{video.Id}/master.mp4";

        var file = new VideoFile
        {
            VideoId = video.Id,
            Path = objectName,
            Type = "master",
            Quality = null,
            CreatedAt = video.CreatedAt,
        };
        
        videoFileRepository.Add(file);
        
        await videoObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
        var url = await videoObjectStorageRepository.GetPresignedUploadUrl(objectName);
        
        await videoRepository.SaveChanges(cancellationToken);
        await videoFileRepository.SaveChanges(cancellationToken);
        return new CreateVideoModel
        {
            VideoId = video.Id,
            UploadUrl = url,
            ObjectKey = objectName,
        };
    }

    public async Task UploadCompete(Guid videoId, CancellationToken cancellationToken)
    {
        /*var video = await videoRepository.GetById(videoId, cancellationToken);
        if (video is null)
        {
            throw new FileEntityNotFoundException($"Видео с идентификатором {videoId} не найдено");
        }*/

        //video.Status = Status.Uploaded;
        await messageProducer.SendMessage("video-processing", "aboba");

    }

    public async Task<string> GetPresignedDownloadUrl(string name, CancellationToken cancellationToken)
    {
        try
        {
            await CheckEntityByName(name, cancellationToken);
            
            await videoObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
            var url = await videoObjectStorageRepository.GetPresignedDownloadUrl(name);
            return url;
        }
        catch (ObjectNotFoundException ex)
        {
            throw new VideoHostingApi.FileService.Service.Exceptions.ObjectNotFoundException($"Видео с названием {name} не найден в S3 хранилище");
        }
    }

    public async Task UploadFile(AddFileModel addFileModel, CancellationToken cancellationToken)
    {
        await EntityIsExists(addFileModel.Name, cancellationToken);
        /*var video = new VideoFile
        {
            ContentType = addFileModel.ContentType,
            Name = addFileModel.Name,
            UploadedAt = DateTime.UtcNow,
            Size = addFileModel.Size,
            UserId = addFileModel.UserId
        };*/
        
        //videoRepository.Add(video);
        await videoRepository.SaveChanges(cancellationToken);
        
        await videoObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
        await videoObjectStorageRepository.UploadFile(addFileModel.Name, addFileModel.FileStream, addFileModel.ContentType, cancellationToken);
    }

    public async Task<FileModel> DownloadFile(string name, CancellationToken cancellationToken)
    {
        try
        {
            await CheckEntityByName(name, cancellationToken);
            
            await videoObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
            var model = await videoObjectStorageRepository.DownloadFile(name, cancellationToken);
            return new FileModel{ FileStream = model.FileStream, ContentType = model.ContentType, FileName = model.FileName }; // TODO: Заменить на автомаппинг
        }
        catch (ObjectNotFoundException ex)
        {
            throw new VideoHostingApi.FileService.Service.Exceptions.ObjectNotFoundException($"Видео с названием {name} не найден в S3 хранилище");
        }
    }

    public async Task<List<string>> GetListObjects(CancellationToken cancellationToken)
    {
        var result = await videoObjectStorageRepository.GetListObjects(cancellationToken);
        return result;
    }

    public async Task DeleteFile(string name, CancellationToken cancellationToken)
    {
        await CheckEntityByName(name, cancellationToken);
        
        try
        {
            await videoObjectStorageRepository.DeleteFile(name, cancellationToken);
        }
        catch (ObjectNotFoundException ex)
        {
            throw new VideoHostingApi.FileService.Service.Exceptions.ObjectNotFoundException($"Видео с названием {name} не найден в S3 хранилище");
        }
    }

    public async Task<FileMetadata> GetMetadata(string fileName, CancellationToken cancellationToken)
    {
        var video = await videoRepository.GetByName(fileName, cancellationToken);
        if (video is null)
        {
            throw new FileEntityNotFoundException($"Сущность видео с названием {fileName} не найдена");
        }
        return mapper.Map<FileMetadata>(video);
    }

    public async Task<IEnumerable<FileMetadata>> GetAllMetadata(CancellationToken cancellationToken)
    {
        var video = await videoRepository.GetAll(cancellationToken);
        return mapper.Map<IEnumerable<FileMetadata>>(video);
    }

    private async Task CheckEntityByName(string name, CancellationToken cancellationToken)
    {
        var video = await videoRepository.GetByName(name, cancellationToken);
        if (video is null)
        {
            throw new FileEntityNotFoundException($"Сущность с названием {name} не найдена");
        }
    }

    private async Task EntityIsExists(string name, CancellationToken cancellationToken)
    {
        var video = await videoRepository.GetByName(name, cancellationToken);
        if (video is not null)
        {
            throw new FileEntityIsExist($"Сущность с названием {name} уже существует");
        }   
    }
}