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
    public class PlacementTestController : Controller
    {
        private readonly AppDbContext _context;

        public PlacementTestController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PlacementTest
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PlacementTests.Include(p => p.Language).Include(p => p.Level);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PlacementTest/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placementTest = await _context.PlacementTests
                .Include(p => p.Language)
                .Include(p => p.Level)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placementTest == null)
            {
                return NotFound();
            }

            return View(placementTest);
        }

        // GET: PlacementTest/Create
        public IActionResult Create()
        {
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription");
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription");
            return View();
        }

        // POST: PlacementTest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanguageId,LevelId,PlacementTestName,PlacementTestDescription,PlacementTestGrade,PlacementTestPassed,Id")] PlacementTest placementTest)
        {
            if (ModelState.IsValid)
            {
                placementTest.Id = Guid.NewGuid();
                _context.Add(placementTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription", placementTest.LanguageId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription", placementTest.LevelId);
            return View(placementTest);
        }

        // GET: PlacementTest/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placementTest = await _context.PlacementTests.FindAsync(id);
            if (placementTest == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription", placementTest.LanguageId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription", placementTest.LevelId);
            return View(placementTest);
        }

        // POST: PlacementTest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LanguageId,LevelId,PlacementTestName,PlacementTestDescription,PlacementTestGrade,PlacementTestPassed,Id")] PlacementTest placementTest)
        {
            if (id != placementTest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(placementTest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlacementTestExists(placementTest.Id))
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
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription", placementTest.LanguageId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription", placementTest.LevelId);
            return View(placementTest);
        }

        // GET: PlacementTest/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placementTest = await _context.PlacementTests
                .Include(p => p.Language)
                .Include(p => p.Level)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placementTest == null)
            {
                return NotFound();
            }

            return View(placementTest);
        }

        // POST: PlacementTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var placementTest = await _context.PlacementTests.FindAsync(id);
            if (placementTest != null)
            {
                _context.PlacementTests.Remove(placementTest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlacementTestExists(Guid id)
        {
            return _context.PlacementTests.Any(e => e.Id == id);
        }
    }
}
