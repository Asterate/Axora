using System.Collections.Generic;
using App.Domain.Entities;

namespace WebApp.ViewModels;

public class AdminDashboardViewModel
{
    public int TotalUsers { get; set; }
    public int TotalInstitutes { get; set; }
    public int TotalLabs { get; set; }
    public int TotalProjects { get; set; }
    
    public List<Institute> RecentInstitutes { get; set; } = new();
    public List<Project> RecentProjects { get; set; } = new();
}