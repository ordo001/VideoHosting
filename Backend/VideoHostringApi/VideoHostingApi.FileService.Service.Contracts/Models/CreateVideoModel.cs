namespace VideoHostingApi.FileService.Service.Contracts.Models;

/// <summary>
/// Модель для добавления видео после создания сущностей
/// </summary>
public class CreateVideoModel
{
    /// <summary>
    /// Идентификатор сущности видео
    /// </summary>
    public Guid VideoId { get; set; }
    
    /// <summary>
    /// Url для загрузки
    /// </summary>
    public string UploadUrl { get; set; } =  string.Empty;
    
    /// <summary>
    /// Ключ объекта
    /// </summary>
    public string ObjectKey { get; set; } = string.Empty;
}