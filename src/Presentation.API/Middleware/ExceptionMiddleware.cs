using System.Net;
using System.Text.Json;

namespace Presentation.API.Middleware;

/// <summary>
/// Global exception handler. Emits RFC 7807 Problem Details (application/problem+json).
/// Stack traces are never written to the response body.
/// </summary>
public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Recurso no encontrado: {Message}", ex.Message);
            await WriteProblemAsync(
                context,
                HttpStatusCode.NotFound,
                "Recurso No Encontrado",
                ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Operación inválida de negocio: {Message}", ex.Message);
            await WriteProblemAsync(
                context,
                HttpStatusCode.BadRequest,
                "Solicitud Inválida",
                ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error crítico interno no controlado en el servidor.");
            await WriteProblemAsync(
                context,
                HttpStatusCode.InternalServerError,
                "Error Interno del Servidor",
                "Ocurrió un error inesperado al procesar la solicitud.");
        }
    }

    private static async Task WriteProblemAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string title,
        string detail)
    {
        if (context.Response.HasStarted)
            throw new InvalidOperationException("Cannot write problem details; response has already started.");

        context.Response.Clear();
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)statusCode;

        var problem = new
        {
            type = $"https://httpstatuses.com/{(int)statusCode}",
            title,
            status = (int)statusCode,
            detail,
            instance = context.Request.Path.Value
        };

        var json = JsonSerializer.Serialize(problem);
        await context.Response.WriteAsync(json);
    }
}
