using App.Domain.Entities;

namespace WebApp.ViewModels;

public class HomeViewModel
{
    public List<Course> PublicCourses { get; set; } = new List<Course>();
    public bool IsAuthenticated { get; set; }
    public string? UserName { get; set; }
}
