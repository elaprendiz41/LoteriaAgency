using Core.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers;

/// <summary>
/// Triggers typed exceptions so ExceptionMiddleware can be verified via Swagger/Postman.
/// </summary>
[ApiController]
[Route("api/demo/errors")]
public sealed class ErrorsController : ControllerBase
{
    private readonly IDemoErrorService _demoErrorService;

    public ErrorsController(IDemoErrorService demoErrorService)
    {
        _demoErrorService = demoErrorService;
    }

    /// <summary>Simulates KeyNotFoundException → 404 problem+json.</summary>
    [HttpGet("not-found")]
    public IActionResult NotFoundError()
    {
        _demoErrorService.ThrowNotFound();
        return Ok();
    }

    /// <summary>Simulates InvalidOperationException → 400 problem+json.</summary>
    [HttpGet("bad-request")]
    public IActionResult BadRequestError()
    {
        _demoErrorService.ThrowInvalidOperation();
        return Ok();
    }

    /// <summary>Simulates unhandled Exception → 500 problem+json (no stack trace).</summary>
    [HttpGet("server-error")]
    public IActionResult ServerError()
    {
        _demoErrorService.ThrowUnhandled();
        return Ok();
    }
}
