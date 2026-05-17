using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Identity;
using App.Modules.Audit.Application.Interface;
using App.Modules.Audit.Application.Services;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Lab.Application.Services;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Services;
using Microsoft.AspNetCore.Identity;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Roles = "admin")]
public class AdminDashboardController : Controller
{
    private readonly IInstituteService _institute;
    private readonly ILabService _lab;
    private readonly IProjectService _project;
    private readonly ISystemLogService _audit;
    private readonly UserManager<AppUser> _userManager;


    public AdminDashboardController(
        IInstituteService instituteService,
        ILabService labService,
        IProjectService projectService,
        ISystemLogService auditService,
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
