using VideoHostingApi.FileService.Repositories.Contracts.Models;

namespace VideoHostingApi.FileService.Repositories.Contracts;

/// <summary>
/// Репозиторий для доступа к данным S3 хранилища Minio
/// </summary>
public interface IMinioRepository<T> where T : class
{
    /// <summary>
    /// Полчить ссылку на загрузку файла в хранилище
    /// </summary>
    public Task<string> GetPresignedUploadUrl(string name);
    
    /// <summary>
    /// Получить ссылку на скачивание файла из хранилища
    /// </summary>
    public Task<string> GetPresignedDownloadUrl(string name);
    
    /// <summary>
    /// Загрузить файл в хранилище
    /// </summary>
    public Task UploadFile(string name, Stream fileStream, string contentType, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить файл из хранилища
    /// </summary>
    public Task<FileDbModel> DownloadFile(string name, CancellationToken cancellationToken);

    /// <summary>
    /// Получить список объектов хранилища
    /// </summary>
    public Task<List<string>> GetListObjects(CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить файл из хранилища
    /// </summary>
    public Task DeleteFile(string name, CancellationToken cancellationToken);
    
    /// <summary>
    /// Существует ли бакет
    /// </summary>
    public Task EnsureBucketExistsAsync(CancellationToken cancellationToken);
}