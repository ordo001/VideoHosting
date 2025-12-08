namespace VideoHostingApi.FileService.Web.Models;

/// <summary>
/// Модель запроса на загрузку видео
/// </summary>
public class UploadVideoRequest
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
    /// Публичное ли видео
    /// </summary>
    public bool IsPublic { get; set; }
}