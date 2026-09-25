namespace Core.Application.Abstractions;

/// <summary>UTC clock abstraction for time-fencing and CreatedAt consistency.</summary>
public interface IClock
{
    DateTime UtcNow { get; }
}
