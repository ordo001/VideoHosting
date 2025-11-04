using VideoHostingApi.FileService.Repositories.Contracts;
using VideoHostingApi.FileService.Service.Contracts.Models;

namespace VideoHostingApi.FileService.Service.Contracts;

/// <summary>
/// Интерфейс для работы с файлами
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Полчить ссылку на загрузку файла в хранилище
    /// </summary>
    public Task<string> GetPresignedUploadUrl(string name, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить ссылку на скачивание файла из хранилища
    /// </summary>
    public Task<string> GetPresignedDownloadUrl(string name, CancellationToken cancellationToken);
    
    /// <summary>
    /// Загрузить видео в хранилище
    /// </summary>
    public Task UploadFile(string name, Stream stream, string contentType, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить ссылку на скачивание видео
    /// </summary>
    public Task<FileModel> DownloadFile(string name, CancellationToken cancellationToken);

    /// <summary>
    /// Получить список объектов хранилища
    /// </summary>
    public Task<List<string>> GetListObjects(CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить видео из хранилища
    /// </summary>
    public Task DeleteFile(string name, CancellationToken cancellationToken);
}