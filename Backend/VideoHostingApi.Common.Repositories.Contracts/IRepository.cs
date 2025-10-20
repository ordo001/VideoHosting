using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.Common.Repositories.Contracts;

/// <summary>
/// Интерфейс репозитория
/// </summary>
public interface IRepository<TEntity> where TEntity : EntityBase
{
    /// <summary>
    /// Получить сущность
    /// </summary>
    public Task<TEntity?> Get(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получить все сущности
    /// </summary>
    public Task<ICollection<TEntity>> GetAll(CancellationToken cancellationToken);

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