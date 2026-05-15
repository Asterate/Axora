using App.Modules.Audit.Application.DTO;
using App.Modules.Project.Application.DTO;

namespace WebApp.ViewModels;

public class AdminDashboardViewModel
{
    public int TotalUsers { get; set; }
    public int TotalInstitutes { get; set; }
    public int TotalLabs { get; set; }
    public int TotalProjects { get; set; }
    
    public IEnumerable<SystemLogResponse> RecentLogs { get; set; } = new List<SystemLogResponse>();
    public IEnumerable<InstituteListResponse> RecentInstitutes { get; set; } =  new List<InstituteListResponse>();
    public IEnumerable<ProjectListResponse> RecentProjects { get; set; } = new List<ProjectListResponse>();
}