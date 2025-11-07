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
public class TokenGenerator(TokenGeneratorOptions options, IUserRepository userRepository) : ITokenGenerator 
{
    public async Task<Token> GenerateToken(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByLogin(loginRequest.Login, cancellationToken);
        if (user is null)
        {
            throw new EntityNotFoundException($"Пользователь с логином {loginRequest.Login} не найден");
        }
            
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new("Login", user.Login),
            new(ClaimTypes.Role, user.Role.Name),
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