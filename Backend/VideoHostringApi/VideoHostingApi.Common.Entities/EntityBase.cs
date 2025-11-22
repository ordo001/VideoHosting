namespace VideoHostingApi.Common.Entities;

/// <summary>
/// Базовый класс для сущностей
/// </summary>
public abstract class EntityBase
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; } =  Guid.NewGuid();
}