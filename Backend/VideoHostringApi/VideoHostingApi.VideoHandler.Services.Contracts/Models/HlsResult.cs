namespace VideoHostingApi.VideoHandler.Services.Contracts.Models;

/// <summary>
/// Результат обработки HLS
/// </summary>
public class HlsResult
{
    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, Stream> Files { get; set; } = new();
}