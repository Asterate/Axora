namespace App.Modules.Project.Application.DTO;

public class ProjectListResponse
{
    public Guid Id { get; set; }
    public string ProjectName { get; set; } = String.Empty;
    public float? Funding { get; set; }
    public string? ProjectTypeName { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ProjectResponse
{
    public Guid Id { get; set; }
    public string ProjectName { get; set; } = String.Empty;
    public float? Funding { get; set; }
    public string? Requirements { get; set; }
    public string? RequirementsFilePath { get; set; }
    public string ProjectTypeName { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; }
}

public class SaveProjectRequest
{
    
    public string ProjectNameEn { get; set; } = String.Empty;
    public string ProjectNameEt { get; set; } = String.Empty;
    public float? Funding { get; set; }
    public string? RequirementsEn { get; set; }
    public string? RequirementsEt { get; set; }
    public string? RequirementsFilePath { get; set; }
    public Guid ProjectTypeId { get; set; }
}