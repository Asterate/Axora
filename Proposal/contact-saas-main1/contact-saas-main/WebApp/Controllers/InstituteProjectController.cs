using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Entities;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Mappers;
using App.Modules.Project.Application.Services;
using App.Modules.Project.Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class InstituteProjectController : Controller
    {
        private readonly InstituteProjectService _instituteProjectService;

        public InstituteProjectController(InstituteProjectService instituteProjectService)
        {
            _instituteProjectService = instituteProjectService;
        }

        // GET: InstituteProject
        public async Task<IActionResult> Index()
        {
            var instituteProjects = _instituteProjectService.GetAllAsync();
            return View(instituteProjects);
        }

        // GET: InstituteProject/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteProject = await _instituteProjectService.GetByIdAsync(id.Value);
            if (instituteProject == null)
            {
                return NotFound();
            }

            return View(instituteProject);
        }

        // GET: InstituteProject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InstituteProject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatedAt,UpdatedAt,DeletedAt,InstituteId,ProjectId,Id")] InstituteProject instituteProject)
        {
            if (ModelState.IsValid)
            {
                var newInstituteProject = new CreateInstituteProjectRequest()
                {
                    InstituteId = instituteProject.InstituteId,
                    ProjectId = instituteProject.ProjectId,
                };
               await _instituteProjectService.CreateAsync(newInstituteProject);
            }
            return View(instituteProject);
        }

        // GET: InstituteProject/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteProject = await _instituteProjectService.GetByIdAsync(id.Value);
            if (instituteProject == null)
            {
                return NotFound();
            }
            return View(instituteProject);
        }

        // POST: InstituteProject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CreatedAt,UpdatedAt,DeletedAt,InstituteId,ProjectId,Id")] InstituteProject instituteProject)
        {
            if (id != instituteProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var instituteProjectUpdate = InstituteProjectMapper.ToUpdateRequest(instituteProject);
                    await _instituteProjectService.UpdateAsync(id, instituteProjectUpdate);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await InstituteProjectExists(instituteProject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(instituteProject);
        }

        // GET: InstituteProject/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteProject = await _instituteProjectService.GetByIdAsync(id.Value);
            if (instituteProject == null)
            {
                return NotFound();
            }

            return View(instituteProject);
        }

        // POST: InstituteProject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var instituteProject = await _instituteProjectService.GetByIdAsync(id);
            if (instituteProject != null)
            {
                await _instituteProjectService.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> InstituteProjectExists(Guid id)
        {
            return await _instituteProjectService.GetByIdAsync(id) != null;
        }
    }
}
