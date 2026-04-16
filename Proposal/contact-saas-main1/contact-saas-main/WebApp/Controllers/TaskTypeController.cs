using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using App.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "admin")]
    public class TaskTypeController : Controller
    {
        private readonly AppDbContext _context;

        public TaskTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TaskType
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaskTypes.ToListAsync());
        }

        // GET: TaskType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskType = await _context.TaskTypes
                .FirstOrDefaultAsync(m => m.Id == id);
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
                
                var taskType = new TaskType
                {
                    Id = Guid.NewGuid(),
                    Name = taskTypeName,
                    Description = taskTypeDescription
                };
                _context.Add(taskType);
                await _context.SaveChangesAsync();
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

            var taskType = await _context.TaskTypes.FindAsync(id);
            if (taskType == null)
            {
                return NotFound();
            }
            
            // Convert entity to ViewModel for display
            var viewModel = new TaskTypeViewModel
            {
                Id = taskType.Id,
                TaskTypeNameEn = taskType.Name.Translate("en") ?? string.Empty,
                TaskTypeNameEt = taskType.Name.Translate("et") ?? string.Empty,
                TaskTypeDescriptionEn = taskType.Description?.Translate("en"),
                TaskTypeDescriptionEt = taskType.Description?.Translate("et")
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
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var taskType = await _context.TaskTypes.FindAsync(id);
                    if (taskType == null)
                    {
                        return NotFound();
                    }
                    
                    // Update translations
                    taskType.Name.SetTranslation(viewModel.TaskTypeNameEn, "en");
                    taskType.Name.SetTranslation(viewModel.TaskTypeNameEt, "et");
                    
                    if (taskType.Description == null)
                    {
                        taskType.Description = new LangStr();
                    }
                    taskType.Description.SetTranslation(viewModel.TaskTypeDescriptionEn ?? string.Empty, "en");
                    taskType.Description.SetTranslation(viewModel.TaskTypeDescriptionEt ?? string.Empty, "et");
                    
                    _context.Update(taskType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskTypeExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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

            var taskType = await _context.TaskTypes
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var taskType = await _context.TaskTypes.FindAsync(id);
            if (taskType != null)
            {
                _context.TaskTypes.Remove(taskType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "LookupData");
        }

        private bool TaskTypeExists(Guid id)
        {
            return _context.TaskTypes.Any(e => e.Id == id);
        }
    }
}
