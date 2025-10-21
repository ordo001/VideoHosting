using Minio;
using VideoHostingApi.FileService.Repositories.Contracts;

namespace VideoHostingApi.FileService.Repositories;

/// <summary>
/// Репозиторий для доступа к S3 хранилищу Minio
/// </summary>
public class MinioRepository<T>(IMinioClient minioClient, string bucketName) : IMinioRepository<T> where T : class 
{
    
    public Task UploadFile(string name, Stream stream)
    {
        throw new NotImplementedException();
    }

    public Task<string> DownloadFile(string name)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetListObjects()
    {
        throw new NotImplementedException();
    }

    public Task DeleteFile(string name)
    {
        throw new NotImplementedException();
    }
}