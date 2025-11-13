using AutoMapper;
using VideoHosting.Auth.Repositories.Contracts;
using VideoHostingApi.Auth.Services.Contracts;
using VideoHostingApi.Auth.Services.Contracts.Exceptions;
using VideoHostingApi.Auth.Services.Contracts.Models;

namespace VideoHostingApi.Auth.Services;

public class AuthService(IUserRepository userRepository,
    ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher, IMapper mapper) : IAuthService, IAuthServiceAnchor
{
    public async Task<Token> LoginAsync(LoginModel loginModel, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByLogin(loginModel.Login, cancellationToken);
        if (user is null)
        {
            throw new InvalidLoginException();
        }
        
        var result = passwordHasher.VerifyHash(loginModel.Password, user.PasswordHash);
        if (!result)
        {
            throw new InvalidLoginException();
        }
        
        var model = mapper.Map<GenerateTokenModel>(user);
        return tokenGenerator.GenerateToken(model);
        
    }
}