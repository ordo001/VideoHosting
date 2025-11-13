using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VideoHosting.Auth.Repositories.Contracts;
using VideoHostingApi.Auth.Services.Contracts;
using VideoHostingApi.Auth.Services.Contracts.Exceptions;
using VideoHostingApi.Auth.Services.Contracts.Models;

namespace VideoHostingApi.Auth.Services;

/// <summary>
/// Генератор токенов
/// </summary>
public class TokenGenerator(TokenGeneratorOptions options,
    IUserRepository userRepository) : ITokenGenerator, IAuthServiceAnchor
{
    public Token GenerateToken(GenerateTokenModel generateTokenModel)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, generateTokenModel.Id.ToString()),
            new("Login", generateTokenModel.Login),
            new(ClaimTypes.Role, generateTokenModel.RoleName),
        };

        var jwt = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddSeconds(options.ExpirationSeconds),
            claims: claims,
            signingCredentials: new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secret)),
                SecurityAlgorithms.HmacSha256)
        );
        
        var  encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        
        return new Token{Value = encodedJwt};
    }
}