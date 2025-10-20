namespace VideoHosting.Common.Repositories.Contracts;

/// <summary>
/// Базовый интерфейс для репозиториев
/// </summary>
public interface IEntityRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Получить сущность <see cref="IEntityRepository{TEntity}"/>
    /// </summary>
    public Task<TEntity?> Get(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить все сущности
    /// </summary>
    public Task<ICollection<TEntity>> GetAll(CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавить сущность
    /// </summary>
    public Task Add(TEntity entity, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновить сущность
    /// </summary>
    public Task Update(TEntity entity, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить сущность
    /// </summary>
    public Task Delete(Guid id, CancellationToken cancellationToken);
    
}