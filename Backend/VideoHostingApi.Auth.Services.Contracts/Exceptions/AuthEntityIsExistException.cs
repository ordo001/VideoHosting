using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.Auth.Services.Contracts.Exceptions;

/// <summary>
/// Сущность уже существует
/// </summary>
public class AuthEntityIsExistException(string message) : ExceptionBase(message);