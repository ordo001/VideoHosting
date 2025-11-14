using Microsoft.EntityFrameworkCore;
using VideoHosting.Auth.Repositories.Contracts;
using VideoHosting.Auth.Repositories.Contracts.Models;
using VideoHostingApi.Common.Repositories;
using VideoHostingApi.Auth.Context;
using VideoHostingApi.Auth.Entities;

namespace VideoHostingApi.Auth.Repositories;

/// <summary>
/// EF Core репозиторий для работы с пользователями
/// </summary>
public class UserRepository(AuthContext context) : WriteRepositoryBase<User>(context),
    IUserRepository, IAuthRepositoryAnchor
{
    public async Task<UserDbModel?> GetByLogin(string login, CancellationToken cancellationToken)
    {
        return await context
            .Set<User>()
            .Select(u => new VideoHosting.Auth.Repositories.Contracts.Models.UserDbModel
            {
                Id = u.Id,
                Login = u.Login,
                PasswordHash = u.PasswordHash,
                Name = u.Name,
                Role = u.Role
            })
            .FirstOrDefaultAsync(x => x.Login == login, cancellationToken);
    }

    public async Task<UserDbModel?> GetById(Guid userId, CancellationToken cancellationToken)
    {
        return await context.Set<User>().Select(u => new UserDbModel
        {
            Id = u.Id,
            Login = u.Login,
            PasswordHash = u.PasswordHash,
            Name = u.Name,
            Role = u.Role
        }).SingleOrDefaultAsync(x => x.Id == userId, cancellationToken);
    }

    public async Task<IEnumerable<UserDbModel>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Set<User>().Select(u => new UserDbModel
        {
            Id = u.Id,
            Login = u.Login,
            PasswordHash = u.PasswordHash,
            Name = u.Name,
            Role = u.Role
        }).ToListAsync(cancellationToken);
    }
}