namespace WebApp.ViewModels;

public class ScheduleDashboardViewModel
{
    public IEnumerable<ScheduleListResponse> Schedules { get; set; } = new List<ScheduleListResponse>();

}