namespace VideoHostingApi.VideoHandler.Services.Contracts.Models;

/// <summary>
/// Результат обработки HLS
/// </summary>
public class HlsResult
{
    //public Dictionary<string, Stream> Files { get; set; } = new();
    /// <summary>
    /// Путь файла в хранилище
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// Поток
    /// </summary>
    public Stream? Stream { get; set; }
    
    /// <summary>
    /// Размер
    /// </summary>
    public long Size { get; set; }
    
    /// <summary>
    /// Тип файла
    /// </summary>
    public string Type { get; set; } = string.Empty;
    
    /// <summary>
    /// Качество
    /// </summary>
    public string? Quality { get; set; } = string.Empty;
}