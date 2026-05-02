using App.DAL.EF;
using App.Domain.Entities;

public class ErrorLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, AppDbContext db)
    {
        await _next(context);

        if (context.Response.StatusCode >= 400)
        {
            db.SystemLogs.Add(new SystemLog
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow,
                Type = "error",
                Message = $"{context.Request.Method} {context.Request.Path}",
                UserName = context.User.Identity?.Name,
                StatusCode = context.Response.StatusCode
            });
            await db.SaveChangesAsync();
        }
    }
}