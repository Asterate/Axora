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
    public class ReagentLabController : Controller
    {
        private readonly AppDbContext _context;

        public ReagentLabController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReagentLab
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ReagentLabs.Include(r => r.Lab).Include(r => r.Reagant);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReagentLab/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reagentLab = await _context.ReagentLabs
                .Include(r => r.Lab)
                .Include(r => r.Reagant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reagentLab == null)
            {
                return NotFound();
            }

            return View(reagentLab);
        }

        // GET: ReagentLab/Create
        public IActionResult Create()
        {
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress");
            ViewData["ReagentId"] = new SelectList(_context.Reagents, "Id", "ReagentDescription");
            return View();
        }

        // POST: ReagentLab/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,Unit,CreatedAt,UpdatedAt,DeletedAt,LabId,ReagentId,Id")] ReagentLab reagentLab)
        {
            if (ModelState.IsValid)
            {
                reagentLab.Id = Guid.NewGuid();
                _context.Add(reagentLab);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress", reagentLab.LabId);
            ViewData["ReagentId"] = new SelectList(_context.Reagents, "Id", "ReagentDescription", reagentLab.ReagentId);
            return View(reagentLab);
        }

        // GET: ReagentLab/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reagentLab = await _context.ReagentLabs.FindAsync(id);
            if (reagentLab == null)
            {
                return NotFound();
            }
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress", reagentLab.LabId);
            ViewData["ReagentId"] = new SelectList(_context.Reagents, "Id", "ReagentDescription", reagentLab.ReagentId);
            return View(reagentLab);
        }

        // POST: ReagentLab/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Quantity,Unit,CreatedAt,UpdatedAt,DeletedAt,LabId,ReagentId,Id")] ReagentLab reagentLab)
        {
            if (id != reagentLab.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reagentLab);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReagentLabExists(reagentLab.Id))
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
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress", reagentLab.LabId);
            ViewData["ReagentId"] = new SelectList(_context.Reagents, "Id", "ReagentDescription", reagentLab.ReagentId);
            return View(reagentLab);
        }

        // GET: ReagentLab/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reagentLab = await _context.ReagentLabs
                .Include(r => r.Lab)
                .Include(r => r.Reagant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reagentLab == null)
            {
                return NotFound();
            }

            return View(reagentLab);
        }

        // POST: ReagentLab/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reagentLab = await _context.ReagentLabs.FindAsync(id);
            if (reagentLab != null)
            {
                _context.ReagentLabs.Remove(reagentLab);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReagentLabExists(Guid id)
        {
            return _context.ReagentLabs.Any(e => e.Id == id);
        }
    }
}
