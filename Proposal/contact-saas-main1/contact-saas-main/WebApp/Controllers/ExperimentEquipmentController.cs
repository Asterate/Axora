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
    public class ExperimentEquipmentController : Controller
    {
        private readonly AppDbContext _context;

        public ExperimentEquipmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ExperimentEquipment
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ExperimentEquipments.Include(e => e.Experiment);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ExperimentEquipment/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentEquipment = await _context.ExperimentEquipments
                .Include(e => e.Experiment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experimentEquipment == null)
            {
                return NotFound();
            }

            return View(experimentEquipment);
        }

        // GET: ExperimentEquipment/Create
        public IActionResult Create()
        {
            ViewData["ExperimentId"] = new SelectList(_context.Experiments, "Id", "ExperimentName");
            return View();
        }

        // POST: ExperimentEquipment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatedAt,UpdatedAt,DeletedAt,ExperimentId,EquipementId,Id")] ExperimentEquipment experimentEquipment)
        {
            if (ModelState.IsValid)
            {
                experimentEquipment.Id = Guid.NewGuid();
                _context.Add(experimentEquipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExperimentId"] = new SelectList(_context.Experiments, "Id", "ExperimentName", experimentEquipment.ExperimentId);
            return View(experimentEquipment);
        }

        // GET: ExperimentEquipment/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentEquipment = await _context.ExperimentEquipments.FindAsync(id);
            if (experimentEquipment == null)
            {
                return NotFound();
            }
            ViewData["ExperimentId"] = new SelectList(_context.Experiments, "Id", "ExperimentName", experimentEquipment.ExperimentId);
            return View(experimentEquipment);
        }

        // POST: ExperimentEquipment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CreatedAt,UpdatedAt,DeletedAt,ExperimentId,EquipementId,Id")] ExperimentEquipment experimentEquipment)
        {
            if (id != experimentEquipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(experimentEquipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperimentEquipmentExists(experimentEquipment.Id))
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
            ViewData["ExperimentId"] = new SelectList(_context.Experiments, "Id", "ExperimentName", experimentEquipment.ExperimentId);
            return View(experimentEquipment);
        }

        // GET: ExperimentEquipment/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentEquipment = await _context.ExperimentEquipments
                .Include(e => e.Experiment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experimentEquipment == null)
            {
                return NotFound();
            }

            return View(experimentEquipment);
        }

        // POST: ExperimentEquipment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var experimentEquipment = await _context.ExperimentEquipments.FindAsync(id);
            if (experimentEquipment != null)
            {
                _context.ExperimentEquipments.Remove(experimentEquipment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperimentEquipmentExists(Guid id)
        {
            return _context.ExperimentEquipments.Any(e => e.Id == id);
        }
    }
}
