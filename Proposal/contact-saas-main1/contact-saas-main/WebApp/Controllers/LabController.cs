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
    public class LabController : Controller
    {
        private readonly AppDbContext _context;

        public LabController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Lab
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Labs.Include(l => l.LabType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Lab/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lab = await _context.Labs
                .Include(l => l.LabType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lab == null)
            {
                return NotFound();
            }

            return View(lab);
        }

        // GET: Lab/Create
        public IActionResult Create()
        {
            ViewData["LabTypeId"] = new SelectList(_context.LabTypes, "Id", "Name");
            return View();
        }

        // POST: Lab/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LabName,LabAddress,LabCapacity,CreatedAt,UpdatedAt,DeletedAt,LabIsActive,LabTypeId,Id")] Lab lab)
        {
            if (ModelState.IsValid)
            {
                lab.Id = Guid.NewGuid();
                _context.Add(lab);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LabTypeId"] = new SelectList(_context.LabTypes, "Id", "Name", lab.LabTypeId);
            return View(lab);
        }

        // GET: Lab/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lab = await _context.Labs.FindAsync(id);
            if (lab == null)
            {
                return NotFound();
            }
            ViewData["LabTypeId"] = new SelectList(_context.LabTypes, "Id", "Name", lab.LabTypeId);
            return View(lab);
        }

        // POST: Lab/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LabName,LabAddress,LabCapacity,CreatedAt,UpdatedAt,DeletedAt,LabIsActive,LabTypeId,Id")] Lab lab)
        {
            if (id != lab.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lab);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabExists(lab.Id))
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
            ViewData["LabTypeId"] = new SelectList(_context.LabTypes, "Id", "Name", lab.LabTypeId);
            return View(lab);
        }

        // GET: Lab/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lab = await _context.Labs
                .Include(l => l.LabType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lab == null)
            {
                return NotFound();
            }

            return View(lab);
        }

        // POST: Lab/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var lab = await _context.Labs.FindAsync(id);
            if (lab != null)
            {
                _context.Labs.Remove(lab);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabExists(Guid id)
        {
            return _context.Labs.Any(e => e.Id == id);
        }
    }
}
