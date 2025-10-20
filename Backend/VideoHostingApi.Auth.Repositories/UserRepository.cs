using VideoHosting.Auth.Repositories.Contracts;
using VideoHosting.Common.Repositories;
using VideoHostingApi.Auth.Context;
using VideoHostingApi.Auth.Entities;

namespace VideoHostingApi.Auth.Repositories;

public class UserRepository(AuthContext context) : RepositoryBase<User>(context),
    IUserRepository, IAuthRepositoryAnchor
{
}