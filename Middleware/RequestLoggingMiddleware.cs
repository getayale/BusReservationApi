using System.Diagnostics;

namespace BusReservation.Api.Middleware;

/// <summary>
/// Logs every incoming request and outgoing response.
/// Adds a correlation ID to help trace a request across logs.
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    // Constructor: receives the next middleware and a logger through dependency injection.
    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Generate a unique ID for this request so related log entries can be matched.
        var correlationId = Guid.NewGuid().ToString("N")[..8];

        // Add the correlation ID to the response before the response starts.
        context.Response.Headers["X-Correlation-Id"] = correlationId;

        // Start measuring how long the request takes to complete.
        var stopwatch = Stopwatch.StartNew();

        // Log information about the incoming request.
        _logger.LogInformation(
            "Incoming Request: {Method} {Path} | CorrelationId: {CorrelationId}",
            context.Request.Method,
            context.Request.Path,
            correlationId);

        // Pass control to the next middleware in the pipeline.
        await _next(context);

        // Stop timing after the rest of the pipeline has finished.
        stopwatch.Stop();

        // Log information about the completed response.
        _logger.LogInformation(
            "Completed Request: Status {StatusCode} | Duration: {ElapsedMs} ms | CorrelationId: {CorrelationId}",
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds,
            correlationId);
    }
}