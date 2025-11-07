using VideoHostingApi.Auth.Services.Contracts.Models;

namespace VideoHostingApi.Auth.Services.Contracts;

/// <summary>
/// Интерфейс сервиса пользователей
/// </summary>
public interface IUserServices
{
    /// <summary>
    /// Получить пользователя пои логину
    /// </summary>
    public Task<UserModel> GetUserByLogin(string login, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить пользователя по идентификатору
    /// </summary>
    public Task<UserModel> GetUserById(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить всех пользователей
    /// </summary>
    public Task<IEnumerable<UserModel>> GetUsers(CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавить пользователя
    /// </summary>
    public Task Add(AddUserModel model, CancellationToken cancellationToken);
}