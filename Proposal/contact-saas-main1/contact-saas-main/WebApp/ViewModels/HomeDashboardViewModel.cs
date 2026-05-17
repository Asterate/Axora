using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using App.Shared.Contracts;

namespace WebApp.ViewModels;

public class HomeDashboardViewModel
{
    public List<LookupItem> ProjectTypes { get; set; } = new();
    public ProjectResponse ProjectResponse { get; set; } = new();
    public SaveProjectRequest  ProjectRequest { get; set; } = new();
    public static async Task<HomeDashboardViewModel> CreateProjectTypes(IProjectTypeService projectTypeService) => new()
    {
        ProjectTypes = await projectTypeService.GetActivesAsync()
    };

    public static async Task<HomeDashboardViewModel> ForEdit(
        SaveProjectRequest project,
        IProjectTypeService projectTypeService) => new()
    {
        ProjectRequest = project,
        ProjectTypes = await projectTypeService.GetActivesAsync()
    };
}