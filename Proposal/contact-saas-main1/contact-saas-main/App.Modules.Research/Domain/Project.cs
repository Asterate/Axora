using App.Shared.Domain;

namespace App.Modules.Project.Domain;

public class Project : BaseEntity
{
    public LangStr ProjectName { get; set; } = "??";

    public float? Funding { get; set; }

    public LangStr? Requirements { get; set; }

    public string? RequirementsFilePath { get; set; }

    public Guid ProjectTypeId { get; set; }
    public ProjectType ProjectType { get; set; } = default!;
    
}