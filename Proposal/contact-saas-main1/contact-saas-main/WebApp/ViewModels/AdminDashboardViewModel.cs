namespace WebApp.ViewModels;

public class AdminDashboardViewModel
{
    public int TotalUsers { get; set; }
    public int TotalInstitutes { get; set; }
    public int TotalLabs { get; set; }
    public int TotalProjects { get; set; }
    
    public IEnumerable<SystemLogListResponse> RecentLogs { get; set; } = new List<SystemLogListResponse>();
    public IEnumerable<InstituteListResponse> RecentInstitutes { get; set; } =  new List<InstituteListResponse>();
    public IEnumerable<ProjectListResponse> RecentProjects { get; set; } = new List<ProjectListResponse>();
}