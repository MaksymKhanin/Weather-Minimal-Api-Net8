using Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Weather_Minimal_Api.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occured: {Message}", exception.Message);

        if (exception is BadHttpRequestException)
        {
            var badRequestProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title =  "Bad Request",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Extensions = new Dictionary<string, object?>
                {
                    { "errors", new[] { exception.Message } }
                }
            };

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await httpContext.Response.WriteAsJsonAsync(badRequestProblemDetails, cancellationToken);

            return true;
        }

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server Error",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
