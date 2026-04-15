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
    public class ReagentController : Controller
    {
        private readonly AppDbContext _context;

        public ReagentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reagent
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Reagents.Include(r => r.ReagentType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Reagent/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reagent = await _context.Reagents
                .Include(r => r.ReagentType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reagent == null)
            {
                return NotFound();
            }

            return View(reagent);
        }

        // GET: Reagent/Create
        public IActionResult Create()
        {
            ViewData["ReagentTypeId"] = new SelectList(_context.ReagentTypes, "Id", "ReagentName");
            return View();
        }

        // POST: Reagent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReagentName,ReagentDescription,CASNumber,ChemicalFormula,MolecularWeight,Concentration,StorageConditions,SafetyNotes,MaterialFilePath,ReagentTypeId,Id")] Reagent reagent)
        {
            if (ModelState.IsValid)
            {
                reagent.Id = Guid.NewGuid();
                _context.Add(reagent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReagentTypeId"] = new SelectList(_context.ReagentTypes, "Id", "ReagentName", reagent.ReagentTypeId);
            return View(reagent);
        }

        // GET: Reagent/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reagent = await _context.Reagents.FindAsync(id);
            if (reagent == null)
            {
                return NotFound();
            }
            ViewData["ReagentTypeId"] = new SelectList(_context.ReagentTypes, "Id", "ReagentName", reagent.ReagentTypeId);
            return View(reagent);
        }

        // POST: Reagent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ReagentName,ReagentDescription,CASNumber,ChemicalFormula,MolecularWeight,Concentration,StorageConditions,SafetyNotes,MaterialFilePath,ReagentTypeId,Id")] Reagent reagent)
        {
            if (id != reagent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reagent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReagentExists(reagent.Id))
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
            ViewData["ReagentTypeId"] = new SelectList(_context.ReagentTypes, "Id", "ReagentName", reagent.ReagentTypeId);
            return View(reagent);
        }

        // GET: Reagent/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reagent = await _context.Reagents
                .Include(r => r.ReagentType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reagent == null)
            {
                return NotFound();
            }

            return View(reagent);
        }

        // POST: Reagent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reagent = await _context.Reagents.FindAsync(id);
            if (reagent != null)
            {
                _context.Reagents.Remove(reagent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReagentExists(Guid id)
        {
            return _context.Reagents.Any(e => e.Id == id);
        }
    }
}
