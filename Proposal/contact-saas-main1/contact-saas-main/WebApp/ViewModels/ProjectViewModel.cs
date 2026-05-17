using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace WebApp.ViewModels;

public class ProjectViewModel
{
    public SaveProjectRequest  ProjectRequest { get; set; } = new();
    public ProjectResponse  RequestResponse { get; set; } = new();
    public List<LookupItem> ProjectTypes { get; set; } = new();
}