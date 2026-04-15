using App.Domain.Entities;

namespace WebApp.ViewModels;

public class ProjectDashboardViewModel
{
    public IEnumerable<Experiment> Experiments { get; set; } = new List<Experiment>();
    public IEnumerable<Schedule> Schedules { get; set; } = new List<Schedule>();
}