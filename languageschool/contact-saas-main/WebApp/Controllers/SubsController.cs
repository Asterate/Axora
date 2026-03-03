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
    public class SubsController : Controller
    {
        private readonly AppDbContext _context;

        public SubsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Subs
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Subs.Include(s => s.Company);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Subs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subs = await _context.Subs
                .Include(s => s.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subs == null)
            {
                return NotFound();
            }

            return View(subs);
        }

        // GET: Subs/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress");
            return View();
        }

        // POST: Subs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,StandardPrice,PremiumPrice,SubsCreatedAt,SubsUpdatedAt,Id")] Subs subs)
        {
            if (ModelState.IsValid)
            {
                subs.Id = Guid.NewGuid();
                _context.Add(subs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress", subs.CompanyId);
            return View(subs);
        }

        // GET: Subs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subs = await _context.Subs.FindAsync(id);
            if (subs == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress", subs.CompanyId);
            return View(subs);
        }

        // POST: Subs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CompanyId,StandardPrice,PremiumPrice,SubsCreatedAt,SubsUpdatedAt,Id")] Subs subs)
        {
            if (id != subs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubsExists(subs.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress", subs.CompanyId);
            return View(subs);
        }

        // GET: Subs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subs = await _context.Subs
                .Include(s => s.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subs == null)
            {
                return NotFound();
            }

            return View(subs);
        }

        // POST: Subs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var subs = await _context.Subs.FindAsync(id);
            if (subs != null)
            {
                _context.Subs.Remove(subs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubsExists(Guid id)
        {
            return _context.Subs.Any(e => e.Id == id);
        }
    }
}
