using AutoMapper;
using VideoHosting.Auth.Repositories.Contracts;
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
            throw new EntityIsExistException($"Пользователь с логином {model.Login} существует");
        }
        await CheckRoleIdOrThrowException(model.RoleId, cancellationToken);
        
        var user = mapper.Map<User>(model); 
        user.PasswordHash = passwordHasher.GeneratePasswordHash(model.Password);
        
        userRepository.Add(user);
        await userRepository.SaveChanges(cancellationToken);
    }

    public async Task Remove(Guid id, CancellationToken cancellationToken)
    {
        var user = await CheckIdOrThrowException(id, cancellationToken); 
        userRepository.Delete(user, cancellationToken);
        await userRepository.SaveChanges(cancellationToken);
    }

    public async Task Update(UpdateUserModel model, CancellationToken cancellationToken)
    {
        var user = await CheckIdOrThrowException(model.Id, cancellationToken);
        await CheckLoginOrThrowException(model.Login, cancellationToken);
        await CheckRoleIdOrThrowException(model.RoleId, cancellationToken);

        user.Login = model.Login;
        user.Name = model.Name;
        user.PasswordHash = passwordHasher.GeneratePasswordHash(model.PasswordHash);
        user.RoleId = model.RoleId;
        
        userRepository.Update(user);
        await userRepository.SaveChanges(cancellationToken);
    }

    private async Task<User> CheckIdOrThrowException(Guid id, CancellationToken cancellationToken)
    {
        var user = await userRepository.Get(id, cancellationToken);
        if (user is null)
        {
            throw new EntityNotFoundException($"Пользователь с идентификатором {id} не найден");
        }
        
        return user;
    }
    
    private async Task<User> CheckLoginOrThrowException(string login, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByLogin(login, cancellationToken);
        if (user is null)
        {
            throw new EntityNotFoundException($"Пользователь с логином {login} не найден");
        }
        
        return user;
    }
    
    private async Task<Role> CheckRoleIdOrThrowException(Guid id, CancellationToken cancellationToken)
    {
        var role = await roleRepository.Get(id, cancellationToken);
        if (role is null)
        {
            throw new EntityNotFoundException($"Роль с идентификатором {id} не найдена");
        }
        return role;
    }
}