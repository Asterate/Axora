using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using App.Shared.Contracts;

namespace WebApp.ViewModels;

public class ProjectDashboardViewModel
{
    public SaveExperimentRequest ExperimentRequest { get; set; } = new();
    public ExperimentResponse ExperimentResponse { get; set; } = new();
    public IEnumerable<ExperimentResponse> ExperimentsResponses { get; set; } = new List<ExperimentResponse>();
    public IEnumerable<SaveExperimentRequest> ExperimentsRequests { get; set; } = new List<SaveExperimentRequest>();
    public IEnumerable<ScheduleListResponse> ScheduleListsResponses { get; set; } = new List<ScheduleListResponse>();
    public IEnumerable<LookupItem> ExperimentTypes { get; set; } = [];
    public IEnumerable<LookupItem> Projects { get; set; } = [];
    
    public static async Task<ProjectDashboardViewModel> ForCreate(IExperimentTypeService experimentTypeService, IProjectService projectService) => new()
    {
        ExperimentTypes = await experimentTypeService.GetActivesAsync(),
        Projects = await projectService.GetActivesAsync()
    };
    public static async Task<ProjectDashboardViewModel> ForEdit(
        SaveExperimentRequest project,
        IExperimentTypeService projectTypeService,
        IProjectService projectService) => new()
    {
        ExperimentRequest = project,
        ExperimentTypes = await projectTypeService.GetActivesAsync(),
        Projects = await projectService.GetActivesAsync()
    };

    public static async Task<ProjectDashboardViewModel> CreateInitials(IScheduleService scheduleService,
        IExperimentTypeService experimentTypeService) => new()
    {
        ScheduleListsResponses = await scheduleService.GetAllAsync(),
        ExperimentTypes = await experimentTypeService.GetActivesAsync(),
    };
}