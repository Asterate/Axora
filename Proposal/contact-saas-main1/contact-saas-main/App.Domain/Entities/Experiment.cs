using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Resources;

namespace App.Domain.Entities;

public class Experiment : BaseEntity
{
    [Display(Name = "Name", ResourceType = typeof(Base.Resources.Common))]
    [StringLength(128, MinimumLength = 3)]
    public string ExperimentName { get; set; }  = default!;
    
    [Display(Name = "Notes", ResourceType = typeof(Base.Resources.Common))]
    public string ExperimentNotes { get; set; }  = default!;
    
    [Display(Name = "CreatedAt", ResourceType = typeof(Base.Resources.Common))]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Display(Name = "UpdatedAt", ResourceType = typeof(Base.Resources.Common))]
    public DateTime? UpdatedAt { get; set; }
    
    [Display(Name = "DeletedAt", ResourceType = typeof(Base.Resources.Common))]
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