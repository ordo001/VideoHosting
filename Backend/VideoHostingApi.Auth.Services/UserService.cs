using AutoMapper;
using VideoHosting.Auth.Repositories.Contracts;
using VideoHosting.Auth.Repositories.Contracts.Models;
using VideoHostingApi.Auth.Entities;
using VideoHostingApi.Auth.Services.Contracts;
using VideoHostingApi.Auth.Services.Contracts.Exceptions;
using VideoHostingApi.Auth.Services.Contracts.Models;

namespace VideoHostingApi.Auth.Services;

/// <summary>
/// Сервис по работе с пользователями
/// </summary>
public class UserService(IUserRepository userRepository,
    IRoleRepository roleRepository, PasswordHasher passwordHasher, IMapper mapper) : IUserServices, IAuthServiceAnchor
{
    public async Task<UserModel> GetUserByLogin(string login, CancellationToken cancellationToken)
    {
        var user = await CheckLoginOrThrowException(login, cancellationToken);
        return mapper.Map<UserModel>(user);
    }

    public async Task<UserModel> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var user = await CheckIdOrThrowException(id, cancellationToken);
        return mapper.Map<UserModel>(user);
    }

    public async Task<IEnumerable<UserModel>> GetUsers(CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAll(cancellationToken);
        return mapper.Map<ICollection<UserModel>>(users);
    }

    public async Task Add(AddUserModel model, CancellationToken cancellationToken)
    {
        var result = await userRepository.GetByLogin(model.Login, cancellationToken);
        if (result is not null)
        {
            throw new AuthEntityIsExistException($"Пользователь с логином {model.Login} существует");
        }
        await CheckRoleIdOrThrowException(model.RoleId, cancellationToken);
        
        var user = mapper.Map<User>(model); 
        user.PasswordHash = passwordHasher.GeneratePasswordHash(model.Password);
        
        userRepository.Add(user);
        await userRepository.SaveChanges(cancellationToken);
    }

    public async Task Remove(Guid id, CancellationToken cancellationToken)
    {
        var model = await CheckIdOrThrowException(id, cancellationToken);
        var user = mapper.Map<User>(model);
        userRepository.Delete(user, cancellationToken);
        await userRepository.SaveChanges(cancellationToken);
    }

    public async Task Update(UpdateUserModel model, CancellationToken cancellationToken)
    {
        var result = await userRepository.GetByLogin(model.Login, cancellationToken);
        if (result is not null)
        {
            throw new AuthEntityIsExistException($"Пользователь с логином {model.Login} существует");
        }
        var userModel = await CheckIdOrThrowException(model.Id, cancellationToken);
        await CheckLoginOrThrowException(userModel.Login, cancellationToken);
        await CheckRoleIdOrThrowException(userModel.Role!.Id, cancellationToken);

        userModel.Login = model.Login;
        userModel.Name = model.Name;
        userModel.PasswordHash = passwordHasher.GeneratePasswordHash(model.PasswordHash);
        userModel.Role.Id = model.RoleId;
        
        var user = mapper.Map<User>(userModel);
        userRepository.Update(user);
        await userRepository.SaveChanges(cancellationToken);
    }

    private async Task<UserDbModel> CheckIdOrThrowException(Guid id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetById(id, cancellationToken);
        if (user is null)
        {
            throw new AuthEntityNotFoundException($"Пользователь с идентификатором {id} не найден");
        }
        
        return user;
    }
    
    private async Task<UserDbModel> CheckLoginOrThrowException(string login, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByLogin(login, cancellationToken);
        if (user is null)
        {
            throw new AuthEntityNotFoundException($"Пользователь с логином {login} не найден");
        }
        
        return user;
    }
    
    private async Task CheckRoleIdOrThrowException(Guid id, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetById(id, cancellationToken);
        if (role is null)
        {
            throw new AuthEntityNotFoundException($"Роль с идентификатором {id} не найдена");
        }
    }
}