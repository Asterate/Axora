namespace App.DTO.v1;

public class ExperimentDto
{
    public Guid Id { get; set; }
    public string ExperimentName { get; set; } = default!;
    public string ExperimentNotes { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid ExperimentTypeId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid InstituteUserId { get; set; }
}

public class CreateExperimentDto
{
    public string ExperimentName { get; set; } = default!;
    public string ExperimentNotes { get; set; } = default!;
    public Guid ExperimentTypeId { get; set; }
    public Guid ProjectId { get; set; }
}