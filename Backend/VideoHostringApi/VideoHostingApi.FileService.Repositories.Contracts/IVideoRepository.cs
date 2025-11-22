using VideoHostingApi.Common.Repositories.Contracts;
using VideoHostingApi.FileService.Entities;

namespace VideoHostingApi.FileService.Repositories.Contracts;

/// <summary>
/// Интерфейс для работы с сущностями видео
/// </summary>
public interface IVideoRepository : IWriteRepository<Video>
{
    /// <summary>
    /// Получить сущность видео по идентификатору
    /// </summary>
    public Task<Video?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получить сущность видео по названию
    /// </summary>
    public Task<Video?> GetByName(string name, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить все сущности видео
    /// </summary>
    public Task<IEnumerable<Video>> GetAll(CancellationToken cancellationToken);

}