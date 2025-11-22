namespace VideoHostingApi.Common.Entities;

/// <summary>
/// Базовый класс исключений
/// </summary>
public class ExceptionBase(string message) : Exception(message);