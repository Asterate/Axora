using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class Schedule : BaseEntity
{
    public Guid AvailabilityId { get; set; }
    public Availability Availability { get; set; } = default!;
    public EScheduleType EScheduleType { get; set; } = EScheduleType.Office;
    public DateTime ScheduleStartAt {get; set;}
    public DateTime ScheduleEndAt {get; set;}
    public DateTime ScheduleCreatedAt { get; set; } = DateTime.Now;
    public DateTime ScheduleModifiedAt { get; set; }
    public DateTime ScheduleDeletedAt { get; set; }
    [StringLength(128, MinimumLength = 3)]
    public string ScheduleEnvironment {get; set;} = default!;

}