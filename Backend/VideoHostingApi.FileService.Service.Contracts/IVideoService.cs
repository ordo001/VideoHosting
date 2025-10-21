using VideoHosting.FileSerivce.Entities;
using VideoHostingApi.FileService.Repositories.Contracts;

namespace VideoHostingApi.FileService.Service.Contracts;

/// <summary>
/// Интерфейс для работы с видео
/// </summary>
public interface IVideoService
{
    /// <summary>
    /// Загрузить видео в хранилище
    /// </summary>
    public Task UploadFile(string name, Stream stream);
    
    /// <summary>
    /// Получить ссылку на скачивание видео
    /// </summary>
    public Task<string> DownloadFile(string name);

    /// <summary>
    /// Получить список объектов хранилища
    /// </summary>
    public Task<List<string>> GetListObjects();
    
    /// <summary>
    /// Удалить видео из хранилища
    /// </summary>
    public Task DeleteFile(string name);
}