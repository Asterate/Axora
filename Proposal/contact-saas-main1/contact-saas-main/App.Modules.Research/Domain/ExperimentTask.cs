using App.Domain.Entities;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Project.Domain;

public class ExperimentTask : BaseEntity
{
    public string TaskName { get; set; } = "??";
    public string? TaskDescription { get; set; }
    public EExperimentTaskStatus Status { get; set; } = EExperimentTaskStatus.Pending;
    public int? Priority { get; set; }

    public Guid ExperimentId { get; set; }
    public Experiment Experiment { get; set; } = default!;
    public Guid TaskTypeId { get; set; }
    public ExperimentTaskType ExperimentTaskType { get; set; } = default!;
    public Guid? AssignedUserId { get; set; }
    public EExperimentTaskPriority PriorityType { get; set; } = EExperimentTaskPriority.Low;

    public string? GetTaskName(string? culture = null)
        => TaskName.GetLocalizedValue(culture);

    public string? GetTaskDescription(string? culture = null)
        => TaskDescription.GetLocalizedValue(culture);
}