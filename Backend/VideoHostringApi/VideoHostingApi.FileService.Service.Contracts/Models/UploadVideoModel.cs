namespace VideoHostingApi.FileService.Service.Contracts.Models;

/// <summary>
/// Модель для загрузки видео
/// </summary>
public class UploadVideoModel
{
    /// <summary>
    /// Заголовок видео
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Описание видео
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Публичное ли видео
    /// </summary>
    public bool IsPublic { get; set; }
}