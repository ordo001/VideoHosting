using VideoHostingApi.Auth.Entities;
using VideoHostingApi.Common.Repositories.Contracts;

namespace VideoHosting.Auth.Repositories.Contracts;

/// <summary>
/// Интерфейс для работы с ролями
/// </summary>
public interface IRoleRepository : IRepository<Role>;