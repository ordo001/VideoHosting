using Microsoft.EntityFrameworkCore;
using VideoHosting.Auth.Repositories.Contracts;
using VideoHostingApi.Auth.Context;
using VideoHostingApi.Auth.Entities;
using VideoHostingApi.Common.Repositories;

namespace VideoHostingApi.Auth.Repositories;

public class RoleRepository(AuthContext context) : RepositoryBase<Role>(context),
    IRoleRepository, IAuthRepositoryAnchor;