using Microsoft.EntityFrameworkCore;
using VideoHostingApi.Common.Context;
using VideoHostingApi.Common.Entities;

namespace VideoHosting.Common.Repositories;

public abstract class RepositoryBase<TEntity>(DbContext context) where TEntity : EntityBase
{
    /// <summary>
    /// Получить сущность <see cref="IEntityRepository{TEntity}"/>
    /// </summary>
    public async Task<TEntity?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    /// <summary>
    /// Получить все сущности
    /// </summary>
    public async Task<ICollection<TEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Добавить сущность
    /// </summary>
    public void Add(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
    }

    /// <summary>
    /// Обновить сущность
    /// </summary>
    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }

    /// <summary>
    /// Удалить сущность
    /// </summary>
    public void Delete(TEntity entity, CancellationToken cancellationToken)
    {
        context.Set<TEntity>().Remove(entity);
    }

    /// <summary>
    /// Сохранить изменения
    /// </summary>
    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}