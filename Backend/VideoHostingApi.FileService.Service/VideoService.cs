using Minio.Exceptions;
using VideoHostingApi.FileService.Entities;
using VideoHostingApi.FileService.Repositories.Contracts;
using VideoHostingApi.FileService.Service.Contracts;
using VideoHostingApi.FileService.Service.Contracts.Models;

namespace VideoHostingApi.FileService.Service;

/// <summary>
/// Сервис для работы с видео
/// </summary>
public class VideoService(IObjectStorageRepository<Video> videoObjectStorageRepository ) : IVideoService
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
            await videoObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
            var url = await videoObjectStorageRepository.GetPresignedDownloadUrl(name);
            return url;
        }
        catch (ObjectNotFoundException ex)
        {
            throw new VideoHostingApi.FileService.Service.Exceptions.ObjectNotFoundException($"Видео с именем {name} не найден");
        }
    }

    public async Task UploadFile(string name, Stream stream, string contentType, CancellationToken cancellationToken)
    {
        await videoObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
        await videoObjectStorageRepository.UploadFile(name, stream, contentType, cancellationToken);
    }

    public async Task<FileModel> DownloadFile(string name, CancellationToken cancellationToken)
    {
        try
        {
            await videoObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
            var model = await videoObjectStorageRepository.DownloadFile(name, cancellationToken);
            return new FileModel{ FileStream = model.FileStream, ContentType = model.ContentType, FileName = model.FileName }; // TODO: Заменить на автомаппинг
        }
        catch (ObjectNotFoundException ex)
        {
            throw new VideoHostingApi.FileService.Service.Exceptions.ObjectNotFoundException($"Видео с именем {name} не найден");
        }
    }

    public async Task<List<string>> GetListObjects(CancellationToken cancellationToken)
    {
        var result = await videoObjectStorageRepository.GetListObjects(cancellationToken);
        return result;
    }

    public async Task DeleteFile(string name, CancellationToken cancellationToken)
    {
        // TODO: сделать проверку
        await videoObjectStorageRepository.DeleteFile(name, cancellationToken);
    }
}