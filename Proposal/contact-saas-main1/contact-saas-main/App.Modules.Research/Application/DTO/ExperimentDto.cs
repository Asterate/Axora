using App.Domain.Entities;

public class ExperimentListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ExperimentName { get; set; }
    public string? ExperimentType { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ExperimentResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    
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

public class UpdateExperimentRequest
{
    public Guid Id { get; set; }
    public string? ExperimentName { get; set; }
    public string ExperimentNotes { get; set; }  = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public Guid ExperimentTypeId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid InstituteUserId { get; set; }
    
    public UpdateExperimentRequest(Experiment experiment)
    {
        Id = experiment.Id;
        ExperimentName = experiment.ExperimentName;
        ExperimentNotes = experiment.ExperimentNotes;
        ExperimentTypeId = experiment.ExperimentTypeId;
        ProjectId = experiment.ProjectId;
        InstituteUserId = experiment.InstituteUserId;
    }
}