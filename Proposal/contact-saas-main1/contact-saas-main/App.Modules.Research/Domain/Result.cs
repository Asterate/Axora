using App.Shared.Domain;

namespace App.Modules.Project.Domain;

public class Result : BaseEntity
{
    public string ResultName { get; set; }  = default!;
    public string ResultDescription { get; set; }  = default!;
    public string? MeasurementName { get; set; }
    public string? MeasurementValue { get; set; }
    public string? Unit { get; set; }
    public string? Notes { get; set; }
    public string? FilePath { get; set; }
    
    public Guid ExperimentId { get; set; }
    public Experiment Experiment { get; set; } = default!;
    public Guid ExperimentTaskId { get; set; }
    public ExperimentTask ExperimentTask { get; set; }= default!;
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }= default!;
    public ICollection<DocumentResult> DocumentResults { get; set; } = new List<DocumentResult>();

}