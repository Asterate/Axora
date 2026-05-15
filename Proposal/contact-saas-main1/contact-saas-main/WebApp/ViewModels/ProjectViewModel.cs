using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace WebApp.ViewModels;

public class ProjectViewModel
{
    public UpdateProjectRequest  Request { get; set; } = new();
    public List<LookupItem> ProjectTypes { get; set; } = new();
}