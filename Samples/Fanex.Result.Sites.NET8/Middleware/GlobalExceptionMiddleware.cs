using Fanex.Result.Sites.NET8.Models;
using System.Net;
using System.Text.Json;

namespace Fanex.Result.Sites.NET8.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public GlobalExceptionMiddleware(
        RequestDelegate next, 
        ILogger<GlobalExceptionMiddleware> logger,
        IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ResultCoreException ex)
        {
            await HandleResultCoreExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleGenericExceptionAsync(context, ex);
        }
    }

    private async Task HandleResultCoreExceptionAsync(HttpContext context, ResultCoreException ex)
    {
        _logger.LogError(ex, "ResultCoreException occurred: {Message}", ex.Message);
        
        // Tránh memory leak bằng cách không log toàn bộ exception details
        var errorResponse = new
        {
            Error = "Third party service error",
            Message = _env.IsDevelopment() ? ex.Message : "Service temporarily unavailable",
            TraceId = context.TraceIdentifier,
            Timestamp = DateTime.UtcNow
        };

        context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
        context.Response.ContentType = "application/json";
        
        var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
        
        await context.Response.WriteAsync(jsonResponse);
    }

    private async Task HandleGenericExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, "Unhandled exception occurred");
        
        var errorResponse = new
        {
            Error = "Internal server error",
            Message = _env.IsDevelopment() ? ex.Message : "An error occurred",
            TraceId = context.TraceIdentifier,
            Timestamp = DateTime.UtcNow
        };

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        
        var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
        
        await context.Response.WriteAsync(jsonResponse);
    }
}

public static class GlobalExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionMiddleware>();
    }
}
