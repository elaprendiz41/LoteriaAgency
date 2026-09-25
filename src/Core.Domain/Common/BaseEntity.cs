namespace Core.Domain.Common;

/// <summary>
/// Root for all domain entities. Agnostic of persistence and frameworks.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    /// <summary>UTC creation timestamp.</summary>
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
}
