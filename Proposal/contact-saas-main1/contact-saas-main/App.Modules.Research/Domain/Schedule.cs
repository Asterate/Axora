using App.Domain.Entities;
using App.Shared.Domain;

namespace App.Modules.Project.Domain;

public class Schedule : BaseEntity
{
    public LangStr ScheduleName { get; set; } = default!;
    public LangStr ScheduleDescription { get; set; } = default!;
    public DateTime ScheduleStartTime { get; set; }
    public DateTime ScheduleEndTime { get; set; }
    public LangStr? ColorCode { get; set; }
    public EScheduleStatus Status { get; set; } = EScheduleStatus.Scheduled;
    public Guid LabId { get; set; }
    public Guid InstituteUserId { get; set; }
    public Guid EquipmentId { get; set; }
    public Guid ExperimentId { get; set; }
    public Experiment Experiment { get; set; } = default!;
}