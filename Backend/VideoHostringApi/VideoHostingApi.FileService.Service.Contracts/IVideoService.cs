using VideoHostingApi.FileService.Service.Contracts.Models;

namespace VideoHostingApi.FileService.Service.Contracts;

/// <summary>
/// Интерфейс сервиса видео
/// </summary>
public interface IVideoService
{
    /// <summary>
    /// Полчить ссылку на загрузку файла в хранилище
    /// </summary>
    public Task<CreateVideoModel> GetPresignedUploadUrl(UploadVideoModel uploadVideoModel, CancellationToken cancellationToken);

    /// <summary>
    /// Указать, что видео загружено в S3
    /// </summary>
    public Task UploadCompete(Guid videoId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить ссылку на скачивание файла из хранилища
    /// </summary>
    public Task<string> GetPresignedDownloadUrl(Guid videoId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Загрузить видео в хранилище
    /// </summary>
    public Task<Guid> UploadFile(AddFileModel fileModel, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить hls файла
    /// </summary>
    public Task<HlsModel> GetHlsFile(string path, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить ссылку на скачивание видео
    /// </summary>
    public Task<FileModel> DownloadFile(string name, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить видео из хранилища
    /// </summary>
    public Task DeleteFile(string name, CancellationToken cancellationToken);
    
}