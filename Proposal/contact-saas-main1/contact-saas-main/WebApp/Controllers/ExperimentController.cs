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
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ExperimentController : Controller
    {
        private readonly AppDbContext _context;

        public ExperimentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Experiment
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Experiments.Include(e => e.ExperimentType).Include(e => e.InstituteUser).Include(e => e.Project);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Experiment/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _context.Experiments
                .Include(e => e.ExperimentType)
                .Include(e => e.InstituteUser)
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experiment == null)
            {
                return NotFound();
            }

            return View(experiment);
        }

        // GET: Experiment/Create
        public IActionResult Create()
        {
            ViewData["ExperimentTypeId"] = new SelectList(_context.ExperimentTypes, "Id", "ExperimentTypeName");
            ViewData["InstituteUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "ProjectName");
            return View();
        }

        // POST: Experiment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExperimentName,ExperimentNotes,CreatedAt,UpdatedAt,DeletedAt,ExperimentTypeId,ProjectId,InstituteUserId,Id")] Experiment experiment)
        {
            if (ModelState.IsValid)
            {
                experiment.Id = Guid.NewGuid();
                _context.Add(experiment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExperimentTypeId"] = new SelectList(_context.ExperimentTypes, "Id", "ExperimentTypeName", experiment.ExperimentTypeId);
            ViewData["InstituteUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id", experiment.InstituteUserId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "ProjectName", experiment.ProjectId);
            return View(experiment);
        }

        // GET: Experiment/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _context.Experiments.FindAsync(id);
            if (experiment == null)
            {
                return NotFound();
            }
            ViewData["ExperimentTypeId"] = new SelectList(_context.ExperimentTypes, "Id", "ExperimentTypeName", experiment.ExperimentTypeId);
            ViewData["InstituteUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id", experiment.InstituteUserId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "ProjectName", experiment.ProjectId);
            return View(experiment);
        }

        // POST: Experiment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ExperimentName,ExperimentNotes,CreatedAt,UpdatedAt,DeletedAt,ExperimentTypeId,ProjectId,InstituteUserId,Id")] Experiment experiment)
        {
            if (id != experiment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(experiment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperimentExists(experiment.Id))
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
            ViewData["ExperimentTypeId"] = new SelectList(_context.ExperimentTypes, "Id", "ExperimentTypeName", experiment.ExperimentTypeId);
            ViewData["InstituteUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id", experiment.InstituteUserId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "ProjectName", experiment.ProjectId);
            return View(experiment);
        }

        // GET: Experiment/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _context.Experiments
                .Include(e => e.ExperimentType)
                .Include(e => e.InstituteUser)
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experiment == null)
            {
                return NotFound();
            }

            return View(experiment);
        }

        // POST: Experiment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var experiment = await _context.Experiments.FindAsync(id);
            if (experiment != null)
            {
                _context.Experiments.Remove(experiment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperimentExists(Guid id)
        {
            return _context.Experiments.Any(e => e.Id == id);
        }
    }
}
