using App.Shared.Domain;

namespace App.Modules.Project.Domain;

public class Result : BaseEntity
{
    public LangStr ResultName { get; set; }  = default!;
    public LangStr ResultDescription { get; set; }  = default!;
    public LangStr? MeasurementName { get; set; }
    public float? MeasurementValue { get; set; }
    public LangStr? Unit { get; set; }
    public LangStr? Notes { get; set; }
    public string? FilePath { get; set; }
    
    public Guid ExperimentId { get; set; }
    public Experiment Experiment { get; set; } = default!;
    public Guid ExperimentTaskId { get; set; }
    public ExperimentTask ExperimentTask { get; set; }= default!;
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }= default!;
    public ICollection<DocumentResult> DocumentResults { get; set; } = new List<DocumentResult>();

}