using Minio.Exceptions;
using VideoHostingApi.FileService.Entities;
using VideoHostingApi.FileService.Repositories.Contracts;
using VideoHostingApi.FileService.Service.Contracts;
using VideoHostingApi.FileService.Service.Contracts.Models;

namespace VideoHostingApi.FileService.Service;

/// <summary>
/// Сервис для работы с изображениями
/// </summary>
public class ImageService(IObjectStorageRepository<Image> imageObjectStorageRepository) : IImageService
{
    public async Task<string> GetPresignedUploadUrl(string name, CancellationToken cancellationToken)
    { 
        await imageObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
        var url = await imageObjectStorageRepository.GetPresignedUploadUrl(name);
        return url;
    }

    public async Task<string> GetPresignedDownloadUrl(string name, CancellationToken cancellationToken)
    {
        try
        {
            await imageObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
            var url = await imageObjectStorageRepository.GetPresignedDownloadUrl(name);
            return url;
        }
        catch (ObjectNotFoundException ex)
        {
            throw new VideoHostingApi.FileService.Service.Exceptions.ObjectNotFoundException($"Изображение с именем {name} не найден");
        }
    }

    public async Task UploadFile(string name, Stream stream, string contentType, CancellationToken cancellationToken)
    {
        await imageObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
        await imageObjectStorageRepository.UploadFile(name, stream, contentType, cancellationToken);
    }

    public async Task<FileModel> DownloadFile(string name, CancellationToken cancellationToken)
    {
        try
        {
            await imageObjectStorageRepository.EnsureBucketExistsAsync(cancellationToken);
            var model = await imageObjectStorageRepository.DownloadFile(name, cancellationToken);
            return new FileModel{ FileStream = model.FileStream, ContentType = model.ContentType, FileName = model.FileName }; // TODO: Заменить на автомаппинг
        }
        catch (ObjectNotFoundException ex)
        {
            throw new VideoHostingApi.FileService.Service.Exceptions.ObjectNotFoundException($"Изображение с именем {name} не найден");
        }
    }

    public Task<List<string>> GetListObjects(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteFile(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}