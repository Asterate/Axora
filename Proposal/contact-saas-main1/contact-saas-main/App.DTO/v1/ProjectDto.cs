namespace App.DTO.v1;

/// <summary>
/// Request DTO for creating a new project
/// </summary>
public class CreateProjectRequest
{
    public string ProjectName { get; set; } = default!;
    public float? Funding { get; set; }
    public string? Requirements { get; set; }
    public string? RequirementsFilePath { get; set; }
    public Guid ProjectTypeId { get; set; }
}

/// <summary>
/// Alias for CreateProjectRequest - used by MVC controllers
/// </summary>
public class CreateProjectDto : CreateProjectRequest { }

/// <summary>
/// Request DTO for updating an existing project
/// </summary>
public class UpdateProjectRequest
{
    public string ProjectName { get; set; } = default!;
    public float? Funding { get; set; }
    public string? Requirements { get; set; }
    public string? RequirementsFilePath { get; set; }
    public Guid ProjectTypeId { get; set; }
}

/// <summary>
/// Response DTO for project data
/// </summary>
public class ProjectResponse
{
    public Guid Id { get; set; }
    public string ProjectName { get; set; } = default!;
    public float? Funding { get; set; }
    public string? Requirements { get; set; }
    public string? RequirementsFilePath { get; set; }
    public Guid ProjectTypeId { get; set; }
}

/// <summary>
/// Alias for ProjectResponse - used by MVC views
/// </summary>
public class ProjectDto : ProjectResponse { }