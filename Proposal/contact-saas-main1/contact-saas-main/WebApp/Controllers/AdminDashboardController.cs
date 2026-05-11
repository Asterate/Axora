using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Roles = "admin")]
public class AdminDashboardController : Controller
{
    private readonly InstituteService _institute;
    private readonly LabService _lab;
    private readonly ProjectService _project;
    private readonly SystemLogService _audit;
    private readonly UserManager<AppUser> _userManager;


    public AdminDashboardController(
        InstituteService instituteService,
        LabService labService,
        ProjectService projectService,
        SystemLogService auditService,
        UserManager<AppUser> userManager)
    {
        _institute = instituteService;
        _lab = labService;
        _project = projectService;
        _audit = auditService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var model = new AdminDashboardViewModel
        {
            TotalUsers = await _userManager.Users.CountAsync(),
            TotalInstitutes = await _institute.CountAsync(),
            TotalLabs = await _lab.CountAsync(),
            TotalProjects = await _project.CountAsync(),
            RecentInstitutes = await _institute.GetRecentAsync(5),
            RecentProjects = await _project.GetRecentAsync(5),
            RecentLogs = await _audit.GetRecentAsync(5)
        };

        return View(model);
    }
}
