using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize]
public class HomeDashboardController : Controller
{
    private readonly ProjectService _projectService;
    private readonly ProjectTypeService _projectTypeService;
    
    public HomeDashboardController(ProjectService projectService, ProjectTypeService projectTypeService)
    {
        _projectService = projectService;
        _projectTypeService = projectTypeService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = GetCurrentUserId();
        if (!userId.HasValue)
        {
            return Challenge();
        }

        var projectDtos = await _projectService.GetAllAsync();
        var projects = projectDtos.Select(p => new ProjectResponse
        {
            Id = p.Id,
            ProjectName = p.ProjectName,
            Funding = p.Funding,
            Requirements = p.Requirements
        }).ToList();

        return View("HomeDashboard", projects);
    }

    // GET: HomeDashboard/Create
    public async Task<IActionResult> Create()
    {
        var model = new HomeDashboardViewModel
        {
            ProjectTypes = await _projectTypeService.GetActivesAsync()
        };
        return View(model);
    }

    // POST: HomeDashboard/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HomeDashboardViewModel dto)
    {
        var userId = GetCurrentUserId();
        if (!userId.HasValue) return Challenge();
    
        if (ModelState.IsValid)
        {
            var request = new CreateProjectRequest
            {
                ProjectName = dto.ProjectName,
                Funding = dto.Funding,
                Requirements = dto.Requirements,
                RequirementsFilePath = dto.RequirementsFilePath,
                ProjectTypeId = dto.ProjectTypeId
            };
            await _projectService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }

        dto.ProjectTypes = await _projectTypeService.GetActivesAsync();
        return View(dto);
    }

    private Guid? GetCurrentUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return null;
        }
        return userId;
    }
}
