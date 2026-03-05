using App.Domain.Entities;

namespace WebApp.Areas.Root.ViewModels;

public class CompanyDesktopViewModel
{
    public Company Company { get; set; } = default!;
    public List<Course>? Courses { get; set; }
    public List<Language>? Languages { get; set; }
    public List<Level>? Levels { get; set; }
}