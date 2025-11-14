using VideoHosting.Auth.Repositories.Contracts.Models;
using VideoHostingApi.Auth.Entities;
using VideoHostingApi.Common.Repositories.Contracts;

namespace VideoHosting.Auth.Repositories.Contracts;

/// <summary>
/// Интерфейс для работы с ролями
/// </summary>
public interface IRoleRepository : IWriteRepository<Role>
{
    /// <summary>
    /// Получить роль по идентификатору
    /// </summary>
    public Task<RoleDbModel?> GetById(Guid id, CancellationToken cancellationToken);
}