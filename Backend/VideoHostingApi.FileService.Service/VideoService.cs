using Minio.Exceptions;
using VideoHosting.FileSerivce.Entities;
using VideoHostingApi.FileService.Repositories.Contracts;
using VideoHostingApi.FileService.Service.Contracts;
using VideoHostingApi.FileService.Service.Contracts.Models;

namespace VideoHostingApi.FileService.Service;

public class VideoService(IMinioRepository<Video> minioRepository) : IVideoService
{
    public async Task<string> GetPresignedUploadUrl(string name, CancellationToken cancellationToken)
    { 
        await minioRepository.EnsureBucketExistsAsync(cancellationToken);
        var ulr = await minioRepository.GetPresignedUploadUrl(name);
        return ulr;
    }

    public async Task<string> GetPresignedDownloadUrl(string name, CancellationToken cancellationToken)
    {
        try
        {
            await minioRepository.EnsureBucketExistsAsync(cancellationToken);
            var ulr = await minioRepository.GetPresignedDownloadUrl(name);
            return ulr;
        }
        catch (ObjectNotFoundException ex)
        {
            throw new VideoHostingApi.FileService.Service.Exceptions.ObjectNotFoundException($"Файл с именем {name} не найден");
        }
    }

    public async Task UploadFile(string name, Stream stream, string contentType, CancellationToken cancellationToken)
    {
        await minioRepository.EnsureBucketExistsAsync(cancellationToken);
        await minioRepository.UploadFile(name, stream, contentType, cancellationToken);
    }

    public async Task<FileModel> DownloadFile(string name, CancellationToken cancellationToken)
    {
        try
        {
            await minioRepository.EnsureBucketExistsAsync(cancellationToken);
            var model = await minioRepository.DownloadFile(name, cancellationToken);
            return new FileModel(){ FileStream = model.FileStream, ContentType = model.ContentType, FileName = model.FileName };
        }
        catch (ObjectNotFoundException ex)
        {
            throw new VideoHostingApi.FileService.Service.Exceptions.ObjectNotFoundException($"Файл с именем {name} не найден");
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