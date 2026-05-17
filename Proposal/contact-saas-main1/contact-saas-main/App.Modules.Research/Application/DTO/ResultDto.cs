namespace App.Modules.Project.Application.DTO;

public class ResultListResponse
{
    public Guid Id { get; set; }
    public string ResultName { get; set; } = String.Empty;
    public string? ExperimentName { get; set; }
    public string? ExperimentTaskName { get; set; }
    public string? ProjectName { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ResultResponse
{
    public Guid Id { get; set; }
    public string ResultName { get; set; }  = String.Empty;
    public string ResultDescription { get; set; }  = String.Empty;
    public string? MeasurementName { get; set; }
    public float? MeasurementValue { get; set; }
    public string? Unit { get; set; }
    public string? Notes { get; set; }
    public string? FilePath { get; set; }
    public string? ExperimentName { get; set; }
    public string? ExperimentTaskName { get; set; }
    public string? ProjectName { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class SaveResultRequest
{
    public string ResultNameEn { get; set; }  = String.Empty;
    public string ResultNameEt { get; set; }  = String.Empty;
    public string ResultDescriptionEn { get; set; }  = String.Empty;
    public string ResultDescriptionEt { get; set; }  = String.Empty;
    public string? MeasurementNameEn { get; set; }
    public string? MeasurementNameEt { get; set; }
    public float? MeasurementValue { get; set; }
    public string? UnitEn { get; set; }
    public string? UnitEt { get; set; }
    public string? NotesEn { get; set; }
    public string? NotesEt { get; set; }
    public string? FilePath { get; set; }
    public Guid ExperimentId { get; set; }
    public Guid ProjectId { get; set; }
}