using VideoHosting.Common.Repositories;
using VideoHostingApi.Auth.Entities;
using VideoHostingApi.Common.Repositories.Contracts;

namespace VideoHosting.Auth.Repositories.Contracts;

/// <summary>
/// Репозиторий для работы с <see cref="User"/>
/// </summary>
public interface IUserRepository :  IRepository<User>;