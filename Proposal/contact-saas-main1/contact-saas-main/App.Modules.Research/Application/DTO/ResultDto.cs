namespace App.Modules.Project.Application.DTO;

public class ResultListResponse
{
    public Guid Id { get; set; }
    public string? ResultName { get; set; }
    public string? ExperimentName { get; set; }
    public string? ExperimentTaskName { get; set; }
    public string? ProjectName { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ResultResponse
{
    public Guid Id { get; set; }
    public string ResultName { get; set; }  = default!;
    public string ResultDescription { get; set; }  = default!;
    public string? MeasurementName { get; set; }
    public string? MeasurementValue { get; set; }
    public string? Unit { get; set; }
    public string? Notes { get; set; }
    public string? FilePath { get; set; }
    public string? ExperimentName { get; set; }
    public string? ExperimentTaskName { get; set; }
    public string? ProjectName { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateResultRequest
{
    public string ResultName { get; set; }  = default!;
    public string ResultDescription { get; set; }  = default!;
    public string? MeasurementName { get; set; }
    public string? MeasurementValue { get; set; }
    public string? Unit { get; set; }
    public string? Notes { get; set; }
    public string? FilePath { get; set; }
    public Guid ExperimentId { get; set; }
    public Guid ProjectId { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class UpdateResultRequest :  CreateResultRequest
{
    public Guid Id { get; set; }
}