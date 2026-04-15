using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class ExperimentTask : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string TaskName { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string? TaskDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public EExperimentTaskStatus Status { get; set; } = EExperimentTaskStatus.Pending;
    public int? Priority { get; set; }
    
    public Guid ExperimentId { get; set; }
    public Experiment Experiment { get; set; } = default!;
    public Guid TaskTypeId { get; set; }
    public TaskType TaskType { get; set; } = default!;
    public ICollection<Result> Results { get; set; } = new List<Result>();
    public Guid? AssignedUserId { get; set; }
    public InstituteUser? AssignedUser { get; set; }

}