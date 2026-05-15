namespace App.Modules.Project.Application.DTO;

public class ExperimentResponse
{
    public Guid Id { get; set; }
    public string? ExperimentName { get; set; }
    public string? ExperimentTypeName { get; set; }
    public string ProjectName { get; set; } = "??";
    public Guid InstituteUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateExperimentRequest
{
    public string? ExperimentName { get; set; }
    public string ExperimentNotes { get; set; }  = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public Guid ExperimentTypeId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid InstituteUserId { get; set; }
}

public class UpdateExperimentRequest : CreateExperimentRequest
{
    public Guid Id { get; set; }
}