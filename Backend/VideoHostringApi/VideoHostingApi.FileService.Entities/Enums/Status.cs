namespace VideoHostingApi.FileService.Entities.Enums;

/// <summary>
/// Статус видео
/// </summary>
public enum Status
{
    /// <summary>
    /// Пользователь не загрузил файл
    /// </summary>
    PendingUpload,
    
    /// <summary>
    /// Raw видео загружено в S3 хранилище
    /// </summary>
    Uploaded,
    
    /// <summary>
    /// Видео в обработке
    /// </summary>
    Processing,
    
    /// <summary>
    /// Готово
    /// </summary>
    Ready,
    
    /// <summary>
    /// Ошибка обработки
    /// </summary>
    Failed,
}