using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.BLL.Services;
using App.DTO.v1;
using App.Modules.Research;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
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
            var projects = await _projectService.GetAllAsync(userId.Value);
            return View(projects);
        }

        // GET: Project/Details/5
        public async Task<IActionResult> Details(Guid? id)
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

            // only returns project if it belongs to user's institute
            var project = await _projectService.GetByIdAsync(id.Value, userId.Value);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Project/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectName,Funding,Requirements,RequirementsFilePath,PublicTypeId")] CreateProjectRequest dto)
        {
            var userId = GetCurrentUserId();
            if (!userId.HasValue)
            {
                return Challenge();
            }

            try
            {
                await _projectService.CreateAsync(dto, userId.Value);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }

        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            
            var project = await _projectService.GetByIdAsync(id.Value, userId.Value);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Project/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProjectName,Funding,Requirements,RequirementsFilePath,ProjectTypeId")] UpdateProjectRequest dto)
        {
            // Note: id comes from route, dto doesn't have Id (as per CreateProjectDto definition)
            // The service will validate ownership using the route id parameter

            var userId = GetCurrentUserId();
            if (!userId.HasValue)
            {
                return Challenge();
            }

            try
            {
                var success = await _projectService.UpdateAsync(id, dto, userId.Value);
                if (!success)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
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
            
            var project = await _projectService.GetByIdAsync(id.Value, userId.Value);
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
            var success = await _projectService.DeleteAsync(id, userId.Value);
            if (!success)
            {
                return NotFound();
            }

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
