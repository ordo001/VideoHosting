namespace VideoHostingApi.FileService.Repositories.Contracts;

/// <summary>
/// Репозиторий для доступа к данным S3 хранилища Minio
/// </summary>
public interface IMinioRepository<T> where T : class
{
    /// <summary>
    /// Загрузить файл в хранилище
    /// </summary>
    public Task UploadFile(string name, Stream stream);
    
    /// <summary>
    /// Получить ссылку на скачивание файла
    /// </summary>
    public Task<string> DownloadFile(string name);

    /// <summary>
    /// Получить список объектов хранилища
    /// </summary>
    public Task<List<string>> GetListObjects();
    
    /// <summary>
    /// Удалить файл из хранилища
    /// </summary>
    public Task DeleteFile(string name);
}