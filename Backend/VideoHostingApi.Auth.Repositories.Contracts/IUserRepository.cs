using VideoHostingApi.Common.Repositories;
using VideoHostingApi.Auth.Entities;
using VideoHostingApi.Auth.Repositories.Contracts.Models;
using VideoHostingApi.Common.Repositories.Contracts;

namespace VideoHostingApi.Auth.Repositories.Contracts;

/// <summary>
/// Репозиторий для работы с <see cref="User"/>
/// </summary>
public interface IUserRepository : IWriteRepository<User>
{
    /// <summary>
    /// Получить пользователя по логину
    /// </summary>
    public Task<UserDbModel?> GetByLogin(string login, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить пользователя по идентификатору
    /// </summary>
    public Task<UserDbModel?> GetById(Guid userId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить всех пользователей
    /// </summary>
    public Task<IEnumerable<UserDbModel>> GetAll(CancellationToken cancellationToken);
}