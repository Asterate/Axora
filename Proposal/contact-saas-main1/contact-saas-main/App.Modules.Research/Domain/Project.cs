using System.ComponentModel.DataAnnotations;
using App.Domain.Entities;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Project.Domain;

public class Project : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string ProjectName { get; set; } = "??";

    public float? Funding { get; set; }

    public string? Requirements { get; set; }

    public string? RequirementsFilePath { get; set; }

    public Guid ProjectTypeId { get; set; }
    public ProjectType ProjectType { get; set; } = default!;

    public string? GetProjectName(string? culture = null)
        => ProjectName.GetLocalizedValue(culture);

    public string? GetRequirements(string? culture = null)
        => Requirements.GetLocalizedValue(culture);
    
}