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
    [Authorize(Roles = "admin")]
    public class InstituteLabController : Controller
    {
        private readonly AppDbContext _context;

        public InstituteLabController(AppDbContext context)
        {
            _context = context;
        }

        // GET: InstituteLab
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.InstituteLabs.Include(i => i.Institute).Include(i => i.Lab);
            return View(await appDbContext.ToListAsync());
        }

        // GET: InstituteLab/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteLab = await _context.InstituteLabs
                .Include(i => i.Institute)
                .Include(i => i.Lab)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instituteLab == null)
            {
                return NotFound();
            }

            return View(instituteLab);
        }

        // GET: InstituteLab/Create
        public IActionResult Create()
        {
            ViewData["InstituteId"] = new SelectList(_context.Institutes, "Id", "InstituteAddress");
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress");
            return View();
        }

        // POST: InstituteLab/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatedAt,UpdatedAt,DeletedAt,InstituteId,LabId,Id")] InstituteLab instituteLab)
        {
            if (ModelState.IsValid)
            {
                instituteLab.Id = Guid.NewGuid();
                _context.Add(instituteLab);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstituteId"] = new SelectList(_context.Institutes, "Id", "InstituteAddress", instituteLab.InstituteId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress", instituteLab.LabId);
            return View(instituteLab);
        }

        // GET: InstituteLab/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteLab = await _context.InstituteLabs.FindAsync(id);
            if (instituteLab == null)
            {
                return NotFound();
            }
            ViewData["InstituteId"] = new SelectList(_context.Institutes, "Id", "InstituteAddress", instituteLab.InstituteId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress", instituteLab.LabId);
            return View(instituteLab);
        }

        // POST: InstituteLab/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CreatedAt,UpdatedAt,DeletedAt,InstituteId,LabId,Id")] InstituteLab instituteLab)
        {
            if (id != instituteLab.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instituteLab);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituteLabExists(instituteLab.Id))
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
            ViewData["InstituteId"] = new SelectList(_context.Institutes, "Id", "InstituteAddress", instituteLab.InstituteId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress", instituteLab.LabId);
            return View(instituteLab);
        }

        // GET: InstituteLab/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteLab = await _context.InstituteLabs
                .Include(i => i.Institute)
                .Include(i => i.Lab)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instituteLab == null)
            {
                return NotFound();
            }

            return View(instituteLab);
        }

        // POST: InstituteLab/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var instituteLab = await _context.InstituteLabs.FindAsync(id);
            if (instituteLab != null)
            {
                _context.InstituteLabs.Remove(instituteLab);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstituteLabExists(Guid id)
        {
            return _context.InstituteLabs.Any(e => e.Id == id);
        }
    }
}
