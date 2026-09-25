namespace Core.Application.Abstractions;

/// <summary>
/// Demo service used by Presentation to trigger typed exceptions
/// so ExceptionMiddleware can be verified (Hito 1).
/// </summary>
public interface IDemoErrorService
{
    void ThrowNotFound();
    void ThrowInvalidOperation();
    void ThrowUnhandled();
}
