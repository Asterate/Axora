using App.BLL;
using App.Domain.Entities;

namespace WebApp.ViewModels;

public class CourseDesktopViewModel
{
    public Course Course { get; set; } = default!;
    public List<Material> Materials { get; set; } = new List<Material>();
    public List<Schedule> Schedules { get; set; } = new List<Schedule>();

    public string? NoMaterialsMessage { get; set; }
    public string? NoSchedulesMessage { get; set; }
    public bool IsEnrolled { get; set; } = false;
}
