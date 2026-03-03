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
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Course
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Courses.Include(c => c.Company).Include(c => c.Language).Include(c => c.Level).Include(c => c.Teacher);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Company)
                .Include(c => c.Language)
                .Include(c => c.Level)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Course/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription");
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress");
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,LanguageId,LevelId,TeacherId,CourseName,CourseDescription,CourseCreatedAt,CourseUpdatedAt,CourseDeletedAt,CourseSeason,Id")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.Id = Guid.NewGuid();
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress", course.CompanyId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription", course.LanguageId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription", course.LevelId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", course.TeacherId);
            return View(course);
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress", course.CompanyId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription", course.LanguageId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription", course.LevelId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", course.TeacherId);
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CompanyId,LanguageId,LevelId,TeacherId,CourseName,CourseDescription,CourseCreatedAt,CourseUpdatedAt,CourseDeletedAt,CourseSeason,Id")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress", course.CompanyId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription", course.LanguageId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription", course.LevelId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", course.TeacherId);
            return View(course);
        }

        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Company)
                .Include(c => c.Language)
                .Include(c => c.Level)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(Guid id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
