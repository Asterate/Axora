using System.ComponentModel.DataAnnotations;
using App.Shared.Domain;

namespace App.Domain.Entities;

public class Schedule : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string ScheduleName { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string ScheduleDescription { get; set; } = default!;
    
    public string? ColorCode { get; set; }
    public EScheduleStatus Status { get; set; } = EScheduleStatus.Scheduled;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime StartTime {get; set;}
    public DateTime EndTime {get; set;}
    
    public Guid LabId { get; set; }
    public Guid InstituteUserId { get; set; }
    public Guid EquipmentId { get; set; }
    public Guid ExperimentTaskId { get; set; }
}