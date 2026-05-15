using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Services;
using App.Shared.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "admin")]
    public class ProjectTypeController : Controller
    {
        private readonly ProjectTypeService _projectTypeService;

        public ProjectTypeController(ProjectTypeService projectTypeService)
        {
            _projectTypeService = projectTypeService;
        }

        // GET: ProjectType
        public async Task<IActionResult> Index()
        {
            return View(await _projectTypeService.GetAllAsync());
        }

        // GET: ProjectType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectType = await _projectTypeService.GetByIdAsync(id.Value);
            if (projectType == null)
            {
                return NotFound();
            }

            return View(projectType);
        }

        // GET: ProjectType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var projectType = new CreateProjectTypeRequest
                {
                    NameEn = viewModel.NameEn,
                    NameEt = viewModel.NameEt,
                    DescriptionEn = viewModel.DescriptionEn,
                    DescriptionEt = viewModel.DescriptionEt
                };
                await _projectTypeService.CreateAsync(projectType);
                return RedirectToAction("Index", "LookupData");
            }
            return View(viewModel);
        }

        // GET: ProjectType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectType = await _projectTypeService.GetByIdAsync(id.Value);
            if (projectType == null)
            {
                return NotFound();
            }
            
            // Convert entity to ViewModel for display
            var viewModel = new ProjectTypeViewModel
            {
                Id = projectType.Id,
                NameEn = projectType.NameEn ?? string.Empty,
                NameEt = projectType.NameEt ?? string.Empty,
                DescriptionEn = projectType.DescriptionEn,
                DescriptionEt = projectType.DescriptionEt
            };
            return View(viewModel);
        }

        // POST: ProjectType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProjectTypeViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var name = new LangStr();
                name.SetTranslation(viewModel.NameEn, "en");
                name.SetTranslation(viewModel.NameEt, "et");

                var description = new LangStr();
                description.SetTranslation(viewModel.DescriptionEn ?? string.Empty, "en");
                description.SetTranslation(viewModel.DescriptionEt ?? string.Empty, "et");

                var update = new UpdateProjectTypeRequest
                {
                    Id = Guid.NewGuid(),
                    NameEn = viewModel.NameEn,
                    NameEt = viewModel.NameEt,
                    DescriptionEn = viewModel.DescriptionEn,
                    DescriptionEt = viewModel.DescriptionEt
                };
                await _projectTypeService.UpdateAsync(id, update);
                return RedirectToAction("Index", "LookupData");
            }
            return View(viewModel);
        }

        // GET: ProjectType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectType = await _projectTypeService.GetByIdAsync(id.Value);
            if (projectType == null)
            {
                return NotFound();
            }

            return View(projectType);
        }

        // POST: ProjectType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var projectType = await _projectTypeService.GetByIdAsync(id);
            if (projectType != null)
            {
                await _projectTypeService.DeleteAsync(id);
            }

            return RedirectToAction("Index", "LookupData");
        }
        
    }
}
