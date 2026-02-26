using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities;

public class Schedule : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string ScheduleName { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string ScheduleDescription { get; set; } = default!;
    public enum ScheduleStatus
    {
        Scheduled,
        Completed,
        Cancelled,
        Pending
    }
    public string? ColorCode { get; set; }
    public ScheduleStatus Status { get; set; } = ScheduleStatus.Scheduled;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime StartTime {get; set;}
    public DateTime EndTime {get; set;}
    
    public Guid LabId { get; set; }
    public Lab? Lab { get; set; }
    public Guid InstituteUserId { get; set; }
    public InstituteUser InstituteUser { get; set; } = default!;
    public Guid EquipmentId { get; set; }
    public Equipment? Equipment { get; set; }
    public Guid ExperimentTaskId { get; set; }
    public ExperimentTask ExperimentTask { get; set; } = default!;
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartTime >= EndTime)
        {
            yield return new ValidationResult(
                "EndTime must be after StartTime.",
                new[] { nameof(StartTime), nameof(EndTime) }
            );
        }
    }
}