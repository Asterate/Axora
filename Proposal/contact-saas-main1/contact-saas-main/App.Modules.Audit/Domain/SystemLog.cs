using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class SystemLog
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    [StringLength(128, MinimumLength = 1)]
    public string Type { get; set; } = "";
    [StringLength(128, MinimumLength = 1)]
    public string Message { get; set; } = "";
    [StringLength(128, MinimumLength = 1)]
    public string? UserName { get; set; }
    public int? StatusCode { get; set; }
}