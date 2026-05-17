namespace App.Modules.Project.Application.DTO;

public class ExperimentResponse
{
    public Guid Id { get; set; }
    public string? ExperimentName { get; set; }
    public string? ExperimentTypeName { get; set; }
    public string? ExperimentNotes{ get; set; }
    public string ProjectName { get; set; } = String.Empty;
    public Guid InstituteUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}

public class SaveExperimentRequest
{
    public string ExperimentNameEn { get; set; }= String.Empty;
    public string ExperimentNameEt { get; set; }= String.Empty;
    public string ExperimentNotesEn { get; set; } = String.Empty;
    public string ExperimentNotesEt { get; set; } = String.Empty;
    public Guid ExperimentTypeId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid InstituteUserId { get; set; }
}