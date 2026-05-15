using App.Domain.Entities;
using App.Shared.Domain;

namespace App.Modules.Project.Domain;

public class Experiment : BaseEntity
{
    public string ExperimentName { get; set; }  = default!;
    
    public string ExperimentNotes { get; set; }  = default!;
    
    public Guid ExperimentTypeId { get; set; }
    public ExperimentType ExperimentType { get; set; } = default!;
    public Guid ProjectId { get; set; }
    public Project Projects { get; set; } = default!;
    public Guid InstituteUserId { get; set; }
    public ICollection<ExperimentTask> ExperimentTasks { get; set; } = new List<ExperimentTask>();
    
}