using App.Modules.Audit.Application.DTO;
using App.Modules.Audit.Application.Interface;
using App.Modules.Audit.Application.Services;

namespace WebApp.Setup;

public class ErrorLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ISystemLogService systemLogService)
    {
        await _next(context);

        if (context.Response.StatusCode >= 400)
        {
            await systemLogService.CreateAsync(new CreateSystemLogRequest
            {
                Timestamp = DateTime.UtcNow,
                Type = "error",
                Message = $"{context.Request.Method} {context.Request.Path}",
                UserName = context.User.Identity?.Name,
                StatusCode = context.Response.StatusCode,
                CreatedAt =  DateTime.UtcNow,
            });
        }
    }
}