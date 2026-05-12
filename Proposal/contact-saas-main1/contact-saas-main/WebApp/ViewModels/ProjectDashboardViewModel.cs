namespace WebApp.ViewModels;

public class ProjectDashboardViewModel
{
    public IEnumerable<ExperimentListResponse> Experiments { get; set; } = new List<ExperimentListResponse>();
    public IEnumerable<ScheduleListResponse> Schedules { get; set; } = new List<ScheduleListResponse>();
}