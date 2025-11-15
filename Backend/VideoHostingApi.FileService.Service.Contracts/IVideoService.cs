using VideoHostingApi.FileService.Service.Contracts.Models;

namespace VideoHostingApi.FileService.Service.Contracts;

/// <summary>
/// Интерфейс сервиса видео
/// </summary>
public interface IVideoService : IFileService
{
    public Task<FileModel> GetMetadata(string fileName, CancellationToken cancellationToken);
}