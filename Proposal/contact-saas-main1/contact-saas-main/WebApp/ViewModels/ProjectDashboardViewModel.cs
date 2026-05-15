using App.Modules.Project.Application.DTO;

namespace WebApp.ViewModels;

public class ProjectDashboardViewModel
{
    public IEnumerable<ExperimentResponse> Experiments { get; set; } = new List<ExperimentResponse>();
    public IEnumerable<ScheduleListResponse> Schedules { get; set; } = new List<ScheduleListResponse>();
}