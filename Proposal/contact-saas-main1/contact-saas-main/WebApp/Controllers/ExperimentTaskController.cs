using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Entities;
using App.Modules.Equipment.Application.Mapper;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class ExperimentTaskController : Controller
    {
        private readonly ExperimentTaskService _experimentTask;

        public ExperimentTaskController(ExperimentTaskService experimentTaskService)
        {
            _experimentTask = experimentTaskService;
        }

        // GET: ExperimentTask
        public async Task<IActionResult> Index()
        {
            var experiments = await _experimentTask.GetAllAsync();
            return View(experiments);
        }

        // GET: ExperimentTask/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentTask = await _experimentTask.GetByIdAsync(id.Value);
            if (experimentTask == null)
            {
                return NotFound();
            }

            return View(experimentTask);
        }

        // GET: ExperimentTask/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: ExperimentTask/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskName,TaskDescription,CreatedAt,UpdatedAt,DeletedAt,Status,Priority,ExperimentId,TaskTypeId,AssignedUserId,Id")] ExperimentTask experimentTask)
        {
            if (ModelState.IsValid)
            {
                await _experimentTask.CreateAsync(new CreateExperimentTaskRequest
                {
                    TaskName = experimentTask.TaskName,
                    Status = experimentTask.Status,
                    Priority = experimentTask.Priority,
                    ExperimentId = experimentTask.ExperimentId,
                    TaskTypeId = experimentTask.TaskTypeId,
                    AssignedUserId = experimentTask.AssignedUserId,
                    
                });
            }
            return View(experimentTask);
        }

        // GET: ExperimentTask/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentTask = await _experimentTask.GetByIdAsync(id.Value);
            if (experimentTask == null)
            {
                return NotFound();
            }
            return View(experimentTask);
        }

        // POST: ExperimentTask/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TaskName,TaskDescription,CreatedAt,UpdatedAt,DeletedAt,Status,Priority,ExperimentId,TaskTypeId,AssignedUserId,Id")] ExperimentTask experimentTask)
        {
            if (id != experimentTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var update = new UpdateExperimentTaskRequest(experimentTask);
                    await _experimentTask.UpdateAsync(id, update);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ExperimentTaskExists(experimentTask.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(experimentTask);
        }

        // GET: ExperimentTask/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentTask = await _experimentTask.GetByIdAsync(id.Value);
            if (experimentTask == null)
            {
                return NotFound();
            }

            return View(experimentTask);
        }

        // POST: ExperimentTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var experimentTask = await _experimentTask.GetByIdAsync(id);
            if (experimentTask != null)
            {
                await _experimentTask.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ExperimentTaskExists(Guid id)
        {
            return await _experimentTask.GetByIdAsync(id) != null;
        }
    }
}
