using Microsoft.EntityFrameworkCore;
using VideoHosting.Auth.Repositories.Contracts;
using VideoHostingApi.Common.Repositories;
using VideoHostingApi.Auth.Context;
using VideoHostingApi.Auth.Entities;

namespace VideoHostingApi.Auth.Repositories;

/// <summary>
/// EF Core репозиторий для работы с пользователями
/// </summary>
public class UserRepository(AuthContext context) : RepositoryBase<User>(context),
    IUserRepository, IAuthRepositoryAnchor
{
    public async Task<User?> GetByLogin(string login, CancellationToken cancellationToken)
    {
        return await context.Set<User>().FirstOrDefaultAsync(x => x.Login == login, cancellationToken);
    }
}