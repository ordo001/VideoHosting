using Microsoft.EntityFrameworkCore;
using VideoHostingApi.Auth.Repositories.Contracts;
using VideoHostingApi.Auth.Repositories.Contracts.Models;
using VideoHostingApi.Auth.Context;
using VideoHostingApi.Auth.Entities;
using VideoHostingApi.Common.Repositories;

namespace VideoHostingApi.Auth.Repositories;

public class RoleRepository(AuthContext context) : WriteRepositoryBase<Role>(context),
    IRoleRepository, IAuthRepositoryAnchor
{
    public async Task<RoleDbModel?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Set<Role>().Select(x => new RoleDbModel
        {
            Id = x.Id,
            RoleName = x.Name
        }).FirstOrDefaultAsync(cancellationToken);
    }
}