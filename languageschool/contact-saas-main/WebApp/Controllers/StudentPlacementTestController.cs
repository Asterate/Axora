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
    public class StudentPlacementTestController : Controller
    {
        private readonly AppDbContext _context;

        public StudentPlacementTestController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StudentPlacementTest
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.StudentPlacementTests.Include(s => s.PlacementTest).Include(s => s.Student);
            return View(await appDbContext.ToListAsync());
        }

        // GET: StudentPlacementTest/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentPlacementTest = await _context.StudentPlacementTests
                .Include(s => s.PlacementTest)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentPlacementTest == null)
            {
                return NotFound();
            }

            return View(studentPlacementTest);
        }

        // GET: StudentPlacementTest/Create
        public IActionResult Create()
        {
            ViewData["PlacementTestId"] = new SelectList(_context.PlacementTests, "Id", "PlacementTestDescription");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "StudentAddress");
            return View();
        }

        // POST: StudentPlacementTest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,PlacementTestId,StudentPlacementTestCreatedAt,StudentPlacementTestUpdatedAt,StudentPlacementTestDeletedAt,Id")] StudentPlacementTest studentPlacementTest)
        {
            if (ModelState.IsValid)
            {
                studentPlacementTest.Id = Guid.NewGuid();
                _context.Add(studentPlacementTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlacementTestId"] = new SelectList(_context.PlacementTests, "Id", "PlacementTestDescription", studentPlacementTest.PlacementTestId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "StudentAddress", studentPlacementTest.StudentId);
            return View(studentPlacementTest);
        }

        // GET: StudentPlacementTest/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentPlacementTest = await _context.StudentPlacementTests.FindAsync(id);
            if (studentPlacementTest == null)
            {
                return NotFound();
            }
            ViewData["PlacementTestId"] = new SelectList(_context.PlacementTests, "Id", "PlacementTestDescription", studentPlacementTest.PlacementTestId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "StudentAddress", studentPlacementTest.StudentId);
            return View(studentPlacementTest);
        }

        // POST: StudentPlacementTest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StudentId,PlacementTestId,StudentPlacementTestCreatedAt,StudentPlacementTestUpdatedAt,StudentPlacementTestDeletedAt,Id")] StudentPlacementTest studentPlacementTest)
        {
            if (id != studentPlacementTest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentPlacementTest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentPlacementTestExists(studentPlacementTest.Id))
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
            ViewData["PlacementTestId"] = new SelectList(_context.PlacementTests, "Id", "PlacementTestDescription", studentPlacementTest.PlacementTestId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "StudentAddress", studentPlacementTest.StudentId);
            return View(studentPlacementTest);
        }

        // GET: StudentPlacementTest/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentPlacementTest = await _context.StudentPlacementTests
                .Include(s => s.PlacementTest)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentPlacementTest == null)
            {
                return NotFound();
            }

            return View(studentPlacementTest);
        }

        // POST: StudentPlacementTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var studentPlacementTest = await _context.StudentPlacementTests.FindAsync(id);
            if (studentPlacementTest != null)
            {
                _context.StudentPlacementTests.Remove(studentPlacementTest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentPlacementTestExists(Guid id)
        {
            return _context.StudentPlacementTests.Any(e => e.Id == id);
        }
    }
}
