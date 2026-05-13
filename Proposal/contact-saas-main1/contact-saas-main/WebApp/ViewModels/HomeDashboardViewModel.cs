using App.Shared.Contracts;

namespace WebApp.ViewModels;

public class HomeDashboardViewModel
{
    public List<LookupItem> ProjectTypes { get; set; } = new();
    public string? ProjectName { get; set; }
    public float? Funding { get; set; }
    public string? Requirements { get; set; }
    public string? RequirementsFilePath { get; set; }
    public Guid ProjectTypeId { get; set; }
}