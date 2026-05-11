public class SystemLogListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class SystemLogResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateSystemLogRequest
{
    public Guid Id { get; set; }
}

public class UpdateSystemLogRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}