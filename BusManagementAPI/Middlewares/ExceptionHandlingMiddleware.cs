﻿using LoggerService;

namespace ExceptionHandlingMiddleware;
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly LoggerService.LoggerService _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, LoggerService.LoggerService logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An error occurred.");
        }
    }
}
