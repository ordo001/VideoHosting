using VideoHosting.Auth.Repositories.Contracts.Models;
using VideoHostingApi.Common.Repositories;
using VideoHostingApi.Auth.Entities;
using VideoHostingApi.Common.Repositories.Contracts;

namespace VideoHosting.Auth.Repositories.Contracts;

/// <summary>
/// Репозиторий для работы с <see cref="User"/>
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Получить пользователя по логину
    /// </summary>
    public Task<Models.UserDbModel?> GetByLogin(string login, CancellationToken cancellationToken);
}