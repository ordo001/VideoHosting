using Microsoft.AspNetCore.Identity;
using VideoHostingApi.Auth.Services.Contracts;

namespace VideoHostingApi.Auth.Services;

public class PasswordHasher : IPasswordHasher, IAuthServiceAnchor
{
    public string GeneratePasswordHash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool VerifyHash(string password, string hash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
    }
}