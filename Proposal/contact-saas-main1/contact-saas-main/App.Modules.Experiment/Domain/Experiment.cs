using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Shared.Domain;

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
    public Guid InstituteUserId { get; set; }
    public ICollection<ExperimentTask> ExperimentTasks { get; set; } = new List<ExperimentTask>();
}