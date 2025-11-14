using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.Common.Repositories.Contracts;

/// <summary>
/// Базовый репозиторий для записи сущностей
/// </summary>
public interface IWriteRepository<TEntity> where TEntity : EntityBase
{
    /// <summary>
    /// Добавить сущность
    /// </summary>
    public void Add(TEntity entity);
    
    /// <summary>
    /// Обновить сущность
    /// </summary>
    public void Update(TEntity entity);

    /// <summary>
    /// Удалить сущность
    /// </summary>
    public void Delete(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Сохранить изменения
    /// </summary>
    public Task SaveChanges(CancellationToken cancellationToken);
}