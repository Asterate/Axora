namespace App.DTO.v1;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string ProjectName { get; set; } = default!;
    public float? Funding { get; set; }
    public string? Requirements { get; set; }
    public string? RequirementsFilePath { get; set; }
    public Guid PublicTypeId { get; set; }
}

public class CreateProjectDto
{
    public string ProjectName { get; set; } = default!;
    public float? Funding { get; set; }
    public string? Requirements { get; set; }
    public string? RequirementsFilePath { get; set; }
    public Guid PublicTypeId { get; set; }
}