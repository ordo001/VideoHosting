using Microsoft.EntityFrameworkCore;
using VideoHostingApi.Auth.Entities.Configuration;
using VideoHostingApi.Common.Context;

namespace VideoHostingApi.Auth.Context;

public class AuthContext(DbContextOptions<AuthContext> options) : DbContextBase<IAuthEntityConfiguration>(options);