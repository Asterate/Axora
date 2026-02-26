using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities;

public class Experiment : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string ExperimentName { get; set; }  = default!;
    public string ExperimentNotes { get; set; }  = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public Guid ExperimentTypeId { get; set; }
    public ExperimentType ExperimentType { get; set; } = default!;
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = default!;
    public Guid InstituteUserId { get; set; }
    public InstituteUser InstituteUser { get; set; } = default!;
    public ICollection<ExperimentTask> ExperimentTasks { get; set; } = new List<ExperimentTask>();
    public ICollection<Result> Results { get; set; } = new List<Result>();
}