using LoggerService;

namespace LoggingMiddleware;
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly LoggerService.LoggerService _logger;

    public LoggingMiddleware(RequestDelegate next, LoggerService.LoggerService logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInfo($"Request: {context.Request.Method} {context.Request.Path}");
        await _next(context);
        _logger.LogInfo($"Response: {context.Response.StatusCode}");
    }
}
