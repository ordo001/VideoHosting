using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.Auth.Services.Contracts.Exceptions;

/// <summary>
/// Сущность не найдена
/// </summary>
public class AuthEntityNotFoundException(string message) : ExceptionBase(message);