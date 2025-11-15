using System.Security.Claims;
using Microsoft.Extensions.Logging;
using VideoHostingApi.FileService.Entities;
using VideoHostingApi.FileService.Repositories.Contracts;
using VideoHostingApi.FileService.Service.Contracts;
using VideoHostingApi.FileService.Service.Contracts.Models;
using VideoHostingApi.FileService.Service.Exceptions;
using ObjectNotFoundException = Minio.Exceptions.ObjectNotFoundException;

namespace VideoHostingApi.FileService.Service;

/// <summary>
/// Сервис для работы с видео
/// </summary>
public class VideoService(IObjectStorageRepository<Video> videoObjectStorageRepository, IVideoRepository videoRepository ) : IVideoService
{
    public async Task<string> GetPresignedUploadUrl(string name, CancellationToken cancellationToken)
    { 
        await videoObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
        var url = await videoObjectStorageRepository.GetPresignedUploadUrl(name);
        return url;
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
            throw new VideoHostingApi.FileService.Service.Exceptions.ObjectNotFoundException($"Видео с именем {name} не найден в S3 хранилище");
        }
    }

    public async Task UploadFile(AddFileModel addFileModel, CancellationToken cancellationToken)
    {
        await EntityIsExists(addFileModel.FileName, cancellationToken);
        var video = new Video
        {
            ContentType = addFileModel.ContentType,
            Name = addFileModel.FileName,
            UploadedAt = DateTime.UtcNow,
            UserId = addFileModel.UserId
        };
        
        await videoObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
        await videoObjectStorageRepository.UploadFile(addFileModel.FileName, addFileModel.FileStream, addFileModel.ContentType, cancellationToken);
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
            throw new VideoHostingApi.FileService.Service.Exceptions.ObjectNotFoundException($"Видео с именем {name} не найден в S3 хранилище");
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
            throw new VideoHostingApi.FileService.Service.Exceptions.ObjectNotFoundException($"Видео с именем {name} не найден в S3 хранилище");
        }
    }

    private async Task CheckEntityByName(string name, CancellationToken cancellationToken)
    {
        var video = await videoRepository.GetByName(name, cancellationToken);
        if (video is null)
        {
            throw new FileEntityNotFoundException($"Сущность с именем {name} не найдена");
        }
    }

    private async Task EntityIsExists(string name, CancellationToken cancellationToken)
    {
        var video = await videoRepository.GetByName(name, cancellationToken);
        if (video is not null)
        {
            throw new FileEntityIsExist($"Сущность с именем {name} уже существует");
        }   
    }
}