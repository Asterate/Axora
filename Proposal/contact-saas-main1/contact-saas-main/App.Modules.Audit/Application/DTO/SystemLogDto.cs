namespace App.Modules.Audit.Application.DTO;

public class SystemLogResponse
{
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public string? Message { get; set; }
    public DateTime Timestamp { get; set; }
    public int StatusCode { get; set; }
    public string? UserName { get; set; }
}

public class CreateSystemLogRequest
{
    public DateTime Timestamp { get; set; }
    public string Type { get; set; } = "";
    public string Message { get; set; } = "";
    public string? UserName { get; set; }
    public int StatusCode { get; set; }
    public DateTime CreatedAt { get; set; }
}