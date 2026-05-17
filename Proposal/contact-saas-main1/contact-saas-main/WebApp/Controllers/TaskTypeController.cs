using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "admin")]
    public class TaskTypeController : Controller
    {
        private readonly IExperimentTaskTypeService _experimentTaskTypeService;

        public TaskTypeController(IExperimentTaskTypeService experimentTaskTypeService)
        {
            _experimentTaskTypeService = experimentTaskTypeService;
        }

        // GET: TaskType
        public async Task<IActionResult> Index()
        {
            return View(await _experimentTaskTypeService.GetAllAsync());
        }

        // GET: TaskType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskType = await _experimentTaskTypeService.GetByIdAsync(id.Value);
            if (taskType == null)
            {
                return NotFound();
            }

            return View(taskType);
        }

        // GET: TaskType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var taskType = new SaveExperimentTaskTypeRequest
                {
                    NameEn = viewModel.TaskTypeNameEn,
                    NameEt = viewModel.TaskTypeNameEt,
                    DescriptionEn = viewModel.TaskTypeDescriptionEn,
                    DescriptionEt = viewModel.TaskTypeDescriptionEt
                };
                await _experimentTaskTypeService.CreateAsync(taskType);
                return RedirectToAction("Index", "LookupData");
            }
            return View(viewModel);
        }

        // GET: TaskType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskType = await _experimentTaskTypeService.GetByIdAsync(id.Value);
            if (taskType == null)
            {
                return NotFound();
            }
            
            // Convert entity to ViewModel for display
            var viewModel = new TaskTypeViewModel
            {//issue
                Id = taskType.Id,
                TaskTypeNameEn = taskType.Name ?? string.Empty,
                TaskTypeNameEt = taskType.Name ?? string.Empty,
                TaskTypeDescriptionEn = taskType.Description,
                TaskTypeDescriptionEt = taskType.Description
            };
            return View(viewModel);
        }

        // POST: TaskType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TaskTypeViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var update = new SaveExperimentTaskTypeRequest
                {
                    NameEn = viewModel.TaskTypeNameEn,
                    NameEt = viewModel.TaskTypeNameEt,
                    DescriptionEn = viewModel.TaskTypeDescriptionEn,
                    DescriptionEt = viewModel.TaskTypeDescriptionEt
                };
                await _experimentTaskTypeService.UpdateAsync(id, update);
                return RedirectToAction("Index", "LookupData");
            }
            return View(viewModel);
        }

        // GET: TaskType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskType = await _experimentTaskTypeService.GetByIdAsync(id.Value);
            if (taskType == null)
            {
                return NotFound();
            }

            return View(taskType);
        }

        // POST: TaskType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var taskType = await _experimentTaskTypeService.GetByIdAsync(id);
            if (taskType != null)
            {
                await _experimentTaskTypeService.DeleteAsync(id);
            }

            return RedirectToAction("Index", "LookupData");
        }
    }
}
