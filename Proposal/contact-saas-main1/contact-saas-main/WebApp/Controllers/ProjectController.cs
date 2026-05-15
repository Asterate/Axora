using System.Security.Claims;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly ProjectService _projectService;
        private readonly ProjectTypeService _projectTypeService;

        public ProjectController(ProjectService projectService, ProjectTypeService projectTypeService)
        {
            _projectService = projectService;
            _projectTypeService = projectTypeService;
        }

        // GET: Project
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            if (!userId.HasValue)
            {
                return Challenge();
            }

            // Get projects filtered by user's institute
            var projects = await _projectService.GetAllAsync();
            return View("~/Views/HomeDashboard/HomeDashboard.cshtml", projects);
        }

        // GET: Project/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null) return NotFound();

            var response = new ProjectResponse
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                Funding = project.Funding,
                Requirements = project.Requirements,
                RequirementsFilePath = project.RequirementsFilePath,
                ProjectTypeId = project.ProjectTypeId
            };

            return View(response);
        }

        // GET: Project/Create
        public async Task<IActionResult> Create()
        {
            var model = new ProjectViewModel
            {
                ProjectTypes = await _projectTypeService.GetActivesAsync()
            };
            return View(model);
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectViewModel model)
        {
            var userId = GetCurrentUserId();
            if (!userId.HasValue) return Challenge();

            try
            {
                await _projectService.CreateAsync(model.Request);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                model.ProjectTypes = await _projectTypeService.GetActivesAsync();
                return View(model);
            }
        }

        // GET: Project/Edit/5
        // GET
        public async Task<IActionResult> Edit(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null) return NotFound();

            var model = new ProjectViewModel
            {
                Request = new UpdateProjectRequest
                {
                    Id = project.Id,
                    ProjectName = project.ProjectName,
                    Funding = project.Funding,
                    Requirements = project.Requirements,
                    RequirementsFilePath = project.RequirementsFilePath,
                    ProjectTypeId = project.ProjectTypeId
                },
                ProjectTypes = await _projectTypeService.GetActivesAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProjectViewModel model)
        {
            
            if (!ModelState.IsValid) return View(model);

            await _projectService.UpdateAsync(id, model.Request);
            return RedirectToAction(nameof(Index));
        }

        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = GetCurrentUserId();
            if (!userId.HasValue)
            {
                return Challenge();
            }
            
            var project = await _projectService.GetByIdAsync(id.Value);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userId = GetCurrentUserId();
            if (!userId.HasValue)
            {
                return Challenge();
            }

            // IDOR protected
            await _projectService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
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
}
