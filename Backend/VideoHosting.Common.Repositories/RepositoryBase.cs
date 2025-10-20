using Microsoft.EntityFrameworkCore;
using VideoHostingApi.Common.Entities;
using VideoHostingApi.Common.Repositories.Contracts;

namespace VideoHosting.Common.Repositories;

/// <summary>
/// Базовый репозиторий для доступа к данных БД
/// </summary>
/// <param name="context">Контекст базы данных</param>
/// <typeparam name="TEntity">Экзепмляр сущности наследуемой от <see cref="EntityBase"/></typeparam>
public abstract class RepositoryBase<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : EntityBase
{
    public async Task<TEntity?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<ICollection<TEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public void Add(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
    }
    
    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }
    
    public void Delete(TEntity entity, CancellationToken cancellationToken)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}