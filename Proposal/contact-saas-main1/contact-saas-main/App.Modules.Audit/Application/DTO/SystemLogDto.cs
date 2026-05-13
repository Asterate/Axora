public class SystemLogListResponse
{
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public string? Message { get; set; }
    public DateTime Timestamp { get; set; }
    public int StatusCode { get; set; }
    public string? UserName { get; set; }
}

public class SystemLogResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateSystemLogRequest
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string Type { get; set; } = "";
    public string Message { get; set; } = "";
    public string? UserName { get; set; }
    public int? StatusCode { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class UpdateSystemLogRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}