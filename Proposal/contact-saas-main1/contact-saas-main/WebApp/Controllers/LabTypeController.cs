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
    public class LabTypeController : Controller
    {
        private readonly AppDbContext _context;

        public LabTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: LabType
        public async Task<IActionResult> Index()
        {
            return View(await _context.LabTypes.ToListAsync());
        }

        // GET: LabType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labType = await _context.LabTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labType == null)
            {
                return NotFound();
            }

            return View(labType);
        }

        // GET: LabType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LabType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Id")] LabType labType)
        {
            if (ModelState.IsValid)
            {
                labType.Id = Guid.NewGuid();
                _context.Add(labType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labType);
        }

        // GET: LabType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labType = await _context.LabTypes.FindAsync(id);
            if (labType == null)
            {
                return NotFound();
            }
            return View(labType);
        }

        // POST: LabType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Id")] LabType labType)
        {
            if (id != labType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabTypeExists(labType.Id))
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
            return View(labType);
        }

        // GET: LabType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labType = await _context.LabTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labType == null)
            {
                return NotFound();
            }

            return View(labType);
        }

        // POST: LabType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var labType = await _context.LabTypes.FindAsync(id);
            if (labType != null)
            {
                _context.LabTypes.Remove(labType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabTypeExists(Guid id)
        {
            return _context.LabTypes.Any(e => e.Id == id);
        }
    }
}
