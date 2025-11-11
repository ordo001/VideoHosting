using VideoHostingApi.Auth.Services.Contracts;
using VideoHostingApi.Auth.Services.Contracts.Models;

namespace VideoHostingApi.Auth.Services;

public class AuthService : IAuthService, IAuthServiceAnchor
{
    public Task<string> LoginAsync(LoginModel loginModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}