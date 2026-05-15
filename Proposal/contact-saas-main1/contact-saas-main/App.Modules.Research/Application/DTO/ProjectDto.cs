namespace App.Modules.Project.Application.DTO;

public class ProjectListResponse
{
    public Guid Id { get; set; }
    public string ProjectName { get; set; } = "??";
    public float? Funding { get; set; }
    public string? ProjectTypeName { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ProjectResponse
{
    public Guid Id { get; set; }
    public string ProjectName { get; set; } = "??";
    public float? Funding { get; set; }
    public string? Requirements { get; set; }
    public string? RequirementsFilePath { get; set; }
    public Guid ProjectTypeId { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateProjectRequest
{
    
    public string ProjectName { get; set; } = "??";
    public float? Funding { get; set; }
    public string? Requirements { get; set; }
    public string? RequirementsFilePath { get; set; }
    public Guid ProjectTypeId { get; set; }
}

public class UpdateProjectRequest :  CreateProjectRequest
{
    public Guid Id { get; set; }
}