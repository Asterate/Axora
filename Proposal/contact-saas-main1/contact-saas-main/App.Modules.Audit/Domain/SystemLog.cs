using App.Shared.Domain;

namespace App.Modules.Audit.Domain;

public class SystemLog : BaseEntity
{
    public DateTime Timestamp { get; set; }
    public string Type { get; set; } = "";
    public string Message { get; set; } = "";
    public string? UserName { get; set; }
    public int StatusCode { get; set; }
}