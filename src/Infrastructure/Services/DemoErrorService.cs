using Core.Application.Abstractions;

namespace Infrastructure.Services;

public sealed class DemoErrorService : IDemoErrorService
{
    public void ThrowNotFound() =>
        throw new KeyNotFoundException("El recurso solicitado no existe.");

    public void ThrowInvalidOperation() =>
        throw new InvalidOperationException("La operación de negocio no es válida.");

    public void ThrowUnhandled() =>
        throw new Exception("Falla crítica simulada para demostrar el middleware.");
}
