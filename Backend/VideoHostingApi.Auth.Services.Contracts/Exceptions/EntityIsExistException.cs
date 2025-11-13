using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.Auth.Services.Contracts.Exceptions;

/// <summary>
/// Сущность уже существует
/// </summary>
public class EntityIsExistException(string message) : ExceptionBase(message);