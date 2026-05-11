using System.ComponentModel.DataAnnotations;
using App.Shared.Domain;

namespace App.Domain.Entities;

public class SystemLog : BaseEntity
{
    public DateTime Timestamp { get; set; }
    [StringLength(128, MinimumLength = 1)]
    public string Type { get; set; } = "";
    [StringLength(128, MinimumLength = 1)]
    public string Message { get; set; } = "";
    [StringLength(128, MinimumLength = 1)]
    public string? UserName { get; set; }
    public int? StatusCode { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
}