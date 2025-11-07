using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.Auth.Services.Contracts.Exceptions;

/// <summary>
/// Сущность не найдена
/// </summary>
public class EntityNotFoundException(string message) : ExceptionBase(message);