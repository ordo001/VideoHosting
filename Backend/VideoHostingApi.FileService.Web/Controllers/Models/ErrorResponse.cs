namespace VideoHostingApi.FileService.Web.Controllers.Models;

/// <summary>
/// Модель ответа при ошибке
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Статусный код ошибки
    /// </summary>
    public int StatusCode { get; set; }
    
    /// <summary>
    /// Сообщение ошибки
    /// </summary>
    public string Message { get; set; } = string.Empty;
}