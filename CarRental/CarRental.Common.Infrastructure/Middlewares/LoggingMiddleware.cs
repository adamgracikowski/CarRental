using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;

namespace CarRental.Common.Infrastructure.Middlewares;

public sealed class LoggingMiddleware : IMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;

    private const string ReceivedRequestLogMessage = "Request: {Method} {Path}";
    private const string ResponseLogMessage = "Response: {StatusCode} {Method} {Path} - {ElapsedMilliseconds}ms";
    private const string ErrorLogMessage = "An unhandled exception occurred while processing the request.";
    private const string InternalServerErrorMessage = "An internal server error occurred. Please try again later.";

    public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            _logger.LogInformation(ReceivedRequestLogMessage,
                context.Request.Method,
                context.Request.Path);

            await next(context);

            _logger.LogInformation(ResponseLogMessage,
                context.Response.StatusCode,
                context.Request.Method,
                context.Request.Path,
                stopwatch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
        finally
        {
            stopwatch.Stop();
        }
    }

    private async Task HandleException(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, ErrorLogMessage);

        if (!context.Response.HasStarted)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                Message = InternalServerErrorMessage
            };

            await context.Response.WriteAsJsonAsync(response);
        }
        else
        {
            _logger.LogWarning("The response has already started, unable to send custom error message.");
        }
    }
}
