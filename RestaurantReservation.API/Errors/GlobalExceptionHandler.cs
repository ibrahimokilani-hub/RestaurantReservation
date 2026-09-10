using Microsoft.AspNetCore.Diagnostics;
using Microsofhtlt.AspNetCore.Http.Features;
using RestaurantReservation.API.Exceptions;

namespace RestaurantReservation.API.Errors;

/// <summary>
/// Translates exceptions into RFC 7807 problem responses. Domain exceptions carry a message that is
/// safe and useful to show a caller; anything else is logged and reported generically.
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<GlobalExceptionHandler> logger)
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title, detail) = exception switch
        {
            NotFoundException notFound => (
                StatusCodes.Status404NotFound,
                "Resource not found",
                notFound.Message),

            ConflictException conflict => (
                StatusCodes.Status409Conflict,
                "Request conflicts with the current state",
                conflict.Message),

            BusinessRuleException businessRule => (
                StatusCodes.Status422UnprocessableEntity,
                "Request could not be processed",
                businessRule.Message),

            _ => (
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred",
                "Something went wrong while processing your request. Please try again later.")
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, "Unhandled exception for {Method} {Path}",
                httpContext.Request.Method, httpContext.Request.Path);
        }
        else
        {
            _logger.LogInformation("{Title}: {Detail}", title, detail);
        }

        httpContext.Response.StatusCode = statusCode;

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails =
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            }
        });
    }
}
