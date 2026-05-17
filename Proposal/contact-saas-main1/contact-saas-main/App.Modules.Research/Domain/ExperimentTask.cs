using App.Domain.Entities;
using App.Shared.Domain;
namespace App.Modules.Project.Domain;

public class ExperimentTask : BaseEntity
{
    public LangStr TaskName { get; set; } = "??";
    public LangStr? TaskDescription { get; set; }
    public EExperimentTaskStatus Status { get; set; } = EExperimentTaskStatus.Pending;
    public int? Priority { get; set; }

    public Guid ExperimentId { get; set; }
    public Experiment Experiment { get; set; } = default!;
    public Guid TaskTypeId { get; set; }
    public ExperimentTaskType ExperimentTaskType { get; set; } = default!;
    public Guid? AssignedUserId { get; set; }
    public EExperimentTaskPriority PriorityType { get; set; } = EExperimentTaskPriority.Low;
}