using System.Security.Claims;
using App.Modules.Project.Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize]
public class HomeDashboardController : Controller
{
    private readonly IProjectService _projectService;
    private readonly IProjectTypeService _projectTypeService;
    
    public HomeDashboardController(IProjectService projectService, IProjectTypeService projectTypeService)
    {
        _projectService = projectService;
        _projectTypeService = projectTypeService;
    }

    public async Task<IActionResult> Index()
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Unauthorized();
        var projects = await _projectService.GetAllAsync(userId);
        return View("HomeDashboard", projects);
    }

    // GET: HomeDashboard/Create
    public async Task<IActionResult> Create()
        => View(await HomeDashboardViewModel.CreateProjectTypes(_projectTypeService));

    // POST: HomeDashboard/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HomeDashboardViewModel dto)
    {
        if (ModelState.IsValid)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
                return Unauthorized();
            await _projectService.CreateAsync(dto.ProjectRequest, userId);
            return RedirectToAction(nameof(Index));
        }
        dto.ProjectTypes = await _projectTypeService.GetActivesAsync();
        return View(dto);
    }
    
    // GET: Project/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        var userId = GetUserId();
        if (userId == null) return Unauthorized();

        var item = await _projectService.GetByIdAsync(id, userId.Value);
        if (item == null) return NotFound();
        return View(item);
    }
    // GET: Project/Edit/5
    // GET
    public async Task<IActionResult> Edit(Guid id)
    {
        var userId = GetUserId();
        if (userId == null) return Unauthorized();

        var project = await _projectService.GetByIdEditAsync(id, userId.Value);
        if (project == null) return NotFound();
        var model = await HomeDashboardViewModel.ForEdit(project, _projectTypeService);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, HomeDashboardViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var userId = GetUserId();
        if (userId == null) return Unauthorized();

        await _projectService.UpdateAsync(id, model.ProjectRequest, userId.Value);
        return RedirectToAction(nameof(Index));
    }
    
    // GET: Project/Delete/5
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();
        if (userId == null) return Unauthorized();

        var project = await _projectService.GetByIdAsync(id, userId.Value);
        if (project == null) return NotFound();
        return View(project);
    }

    // POST: Project/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var userId = GetUserId();
        if (userId == null) return Unauthorized();

        await _projectService.DeleteAsync(id, userId.Value);
        return RedirectToAction(nameof(Index));
    }
    private Guid? GetUserId() =>
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;
}
