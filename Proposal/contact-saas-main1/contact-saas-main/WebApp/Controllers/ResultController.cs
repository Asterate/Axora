using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Entities;

namespace WebApp.Controllers
{
    public class ResultController : Controller
    {
        private readonly AppDbContext _context;

        public ResultController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Result
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Results.Include(r => r.Experiment).Include(r => r.ExperimentTask);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Result/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.Experiment)
                .Include(r => r.ExperimentTask)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Result/Create
        public IActionResult Create()
        {
            ViewData["ExperimentId"] = new SelectList(_context.Experiments, "Id", "ExperimentName");
            ViewData["ExperimentTaskId"] = new SelectList(_context.ExperimentTasks, "Id", "TaskName");
            return View();
        }

        // POST: Result/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultName,ResultDescription,MeasurementName,MeasurementValue,Unit,Notes,FilePath,CreatedAt,UpdatedAt,DeletedAt,ExperimentId,ExperimentTaskId,Id")] Result result)
        {
            if (ModelState.IsValid)
            {
                result.Id = Guid.NewGuid();
                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExperimentId"] = new SelectList(_context.Experiments, "Id", "ExperimentName", result.ExperimentId);
            ViewData["ExperimentTaskId"] = new SelectList(_context.ExperimentTasks, "Id", "TaskName", result.ExperimentTaskId);
            return View(result);
        }

        // GET: Result/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            ViewData["ExperimentId"] = new SelectList(_context.Experiments, "Id", "ExperimentName", result.ExperimentId);
            ViewData["ExperimentTaskId"] = new SelectList(_context.ExperimentTasks, "Id", "TaskName", result.ExperimentTaskId);
            return View(result);
        }

        // POST: Result/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ResultName,ResultDescription,MeasurementName,MeasurementValue,Unit,Notes,FilePath,CreatedAt,UpdatedAt,DeletedAt,ExperimentId,ExperimentTaskId,Id")] Result result)
        {
            if (id != result.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.Id))
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
            ViewData["ExperimentId"] = new SelectList(_context.Experiments, "Id", "ExperimentName", result.ExperimentId);
            ViewData["ExperimentTaskId"] = new SelectList(_context.ExperimentTasks, "Id", "TaskName", result.ExperimentTaskId);
            return View(result);
        }

        // GET: Result/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.Experiment)
                .Include(r => r.ExperimentTask)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Result/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _context.Results.FindAsync(id);
            if (result != null)
            {
                _context.Results.Remove(result);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultExists(Guid id)
        {
            return _context.Results.Any(e => e.Id == id);
        }
    }
}
