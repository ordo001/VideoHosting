using Microsoft.EntityFrameworkCore;
using VideoHostingApi.Common.Entities;
using VideoHostingApi.Common.Repositories.Contracts;

namespace VideoHostingApi.Common.Repositories;

/// <summary>
/// Базовый репозиторий для записи сущностей
/// </summary>
public abstract class WriteRepositoryBase<TEntity>(DbContext context) : IWriteRepository<TEntity>
    where TEntity : EntityBase
{
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