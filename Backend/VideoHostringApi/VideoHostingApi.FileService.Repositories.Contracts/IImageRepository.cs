using VideoHostingApi.FileService.Entities;

namespace VideoHostingApi.FileService.Repositories.Contracts;

/// <summary>
/// Интерфейс для работы с сущностями изображений
/// </summary>
public interface IImageRepository
{
    /// <summary>
    /// Получить изображение по идентификатору
    /// </summary>
    public Task<Image> GetById(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить изображение по названию
    /// </summary>
    public Task<Image> GetByName(string name, CancellationToken cancellationToken);
}