using Microsoft.AspNetCore.Http;

namespace VideoHostingApi.FileService.Web.Models;

/// <summary>
/// Модель для загрузки видео файла через API
/// </summary>
public class UploadFileVideoRequest
{
    /// <summary>
    /// Файл видео
    /// </summary>
    public required IFormFile VideoFile { get; set; }
    
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