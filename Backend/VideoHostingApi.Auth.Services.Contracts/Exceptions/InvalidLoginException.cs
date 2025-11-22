using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.Auth.Services.Contracts.Exceptions;

/// <summary>
/// Неверный логин или пароль exception
/// </summary>
public class InvalidLoginException() : ExceptionBase("Неверный логин или пароль");