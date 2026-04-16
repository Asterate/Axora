using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class ExperimentTaskController : Controller
    {
        private readonly AppDbContext _context;

        public ExperimentTaskController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ExperimentTask
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ExperimentTasks.Include(e => e.AssignedUser).Include(e => e.Experiment).Include(e => e.TaskType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ExperimentTask/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentTask = await _context.ExperimentTasks
                .Include(e => e.AssignedUser)
                .Include(e => e.Experiment)
                .Include(e => e.TaskType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experimentTask == null)
            {
                return NotFound();
            }

            return View(experimentTask);
        }

        // GET: ExperimentTask/Create
        public IActionResult Create()
        {
            ViewData["AssignedUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id");
            ViewData["ExperimentId"] = new SelectList(_context.Experiments, "Id", "ExperimentName");
            ViewData["TaskTypeId"] = new SelectList(_context.TaskTypes, "Id", "TaskTypeName");
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
                experimentTask.Id = Guid.NewGuid();
                _context.Add(experimentTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignedUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id", experimentTask.AssignedUserId);
            ViewData["ExperimentId"] = new SelectList(_context.Experiments, "Id", "ExperimentName", experimentTask.ExperimentId);
            ViewData["TaskTypeId"] = new SelectList(_context.TaskTypes, "Id", "TaskTypeName", experimentTask.TaskTypeId);
            return View(experimentTask);
        }

        // GET: ExperimentTask/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentTask = await _context.ExperimentTasks.FindAsync(id);
            if (experimentTask == null)
            {
                return NotFound();
            }
            ViewData["AssignedUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id", experimentTask.AssignedUserId);
            ViewData["ExperimentId"] = new SelectList(_context.Experiments, "Id", "ExperimentName", experimentTask.ExperimentId);
            ViewData["TaskTypeId"] = new SelectList(_context.TaskTypes, "Id", "TaskTypeName", experimentTask.TaskTypeId);
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
                    _context.Update(experimentTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperimentTaskExists(experimentTask.Id))
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
            ViewData["AssignedUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id", experimentTask.AssignedUserId);
            ViewData["ExperimentId"] = new SelectList(_context.Experiments, "Id", "ExperimentName", experimentTask.ExperimentId);
            ViewData["TaskTypeId"] = new SelectList(_context.TaskTypes, "Id", "TaskTypeName", experimentTask.TaskTypeId);
            return View(experimentTask);
        }

        // GET: ExperimentTask/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentTask = await _context.ExperimentTasks
                .Include(e => e.AssignedUser)
                .Include(e => e.Experiment)
                .Include(e => e.TaskType)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var experimentTask = await _context.ExperimentTasks.FindAsync(id);
            if (experimentTask != null)
            {
                _context.ExperimentTasks.Remove(experimentTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperimentTaskExists(Guid id)
        {
            return _context.ExperimentTasks.Any(e => e.Id == id);
        }
    }
}
