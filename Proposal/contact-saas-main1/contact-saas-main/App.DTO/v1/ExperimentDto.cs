namespace App.DTO.v1;

/// <summary>
/// Request DTO for creating a new experiment
/// </summary>
public class CreateExperimentRequest
{
    public string ExperimentName { get; set; } = default!;
    public string ExperimentNotes { get; set; } = default!;
    public Guid ExperimentTypeId { get; set; }
    public Guid ProjectId { get; set; }
}

/// <summary>
/// Request DTO for updating an experiment
/// </summary>
public class UpdateExperimentRequest
{
    public string ExperimentName { get; set; } = default!;
    public string ExperimentNotes { get; set; } = default!;
    public Guid ExperimentTypeId { get; set; }
    public Guid ProjectId { get; set; }
}

/// <summary>
/// Response DTO for experiment data
/// </summary>
public class ExperimentResponse
{
    public Guid Id { get; set; }
    public string ExperimentName { get; set; } = default!;
    public string ExperimentNotes { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid ExperimentTypeId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid InstituteUserId { get; set; }
}