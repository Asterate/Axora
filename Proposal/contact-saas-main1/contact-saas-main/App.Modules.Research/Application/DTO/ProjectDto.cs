public class ProjectListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ProjectName { get; set; }
    public decimal? Funding { get; set; }
    public string? Requirements { get; set; }
    public string? RequirementsFilePath { get; set; }
    public Guid? ProjectTypeId { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ProjectResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ProjectName { get; set; }
    public decimal? Funding { get; set; }
    public string? Requirements { get; set; }
    public string? RequirementsFilePath { get; set; }
    public Guid? ProjectTypeId { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateProjectRequest
{
    public Guid Id { get; set; }
    public string? ProjectName { get; set; }
    public float? Funding { get; set; }
    public string? Requirements { get; set; }
    public string? RequirementsFilePath { get; set; }
    public Guid ProjectTypeId { get; set; }
}

public class UpdateProjectRequest
{
    public Guid Id { get; set; }
    public string? ProjectName { get; set; }
    public decimal? Funding { get; set; }
    public string? Requirements { get; set; }
    public string? RequirementsFilePath { get; set; }
    public Guid? ProjectTypeId { get; set; }
}