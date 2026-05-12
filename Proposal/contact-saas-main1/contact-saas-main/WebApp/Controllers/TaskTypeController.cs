using Microsoft.AspNetCore.Mvc;
using App.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "admin")]
    public class TaskTypeController : Controller
    {
        private readonly ExperimentTaskTypeService _experimentTaskTypeService;

        public TaskTypeController(ExperimentTaskTypeService experimentTaskTypeService)
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var taskTypeName = new LangStr();
                taskTypeName.SetTranslation(viewModel.TaskTypeNameEn, "en");
                taskTypeName.SetTranslation(viewModel.TaskTypeNameEt, "et");
                
                var taskTypeDescription = new LangStr();
                taskTypeDescription.SetTranslation(viewModel.TaskTypeDescriptionEn ?? string.Empty, "en");
                taskTypeDescription.SetTranslation(viewModel.TaskTypeDescriptionEt ?? string.Empty, "et");
                
                var taskType = new CreateExperimentTaskTypeRequest()
                {
                    Id = Guid.NewGuid(),
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
            {
                Id = taskType.Id,
                TaskTypeNameEn = taskType.Name ?? string.Empty,
                TaskTypeNameEt = taskType.Name ?? string.Empty,
            };
            return View(viewModel);
        }

        // POST: TaskType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TaskTypeViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var name = new LangStr();
                name.SetTranslation(viewModel.TaskTypeNameEn, "en");
                name.SetTranslation(viewModel.TaskTypeNameEt, "et");

                var description = new LangStr();
                description.SetTranslation(viewModel.TaskTypeDescriptionEn ?? string.Empty, "en");
                description.SetTranslation(viewModel.TaskTypeDescriptionEt ?? string.Empty, "et");

                var update = new UpdateExperimentTaskTypeRequest
                {
                    Id = id,
                    Name = name,
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
