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
    public class TeacherLanguageController : Controller
    {
        private readonly AppDbContext _context;

        public TeacherLanguageController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TeacherLanguage
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TeacherLanguages.Include(t => t.Language).Include(t => t.Level).Include(t => t.Teacher);
            return View(await appDbContext.ToListAsync());
        }

        // GET: TeacherLanguage/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherLanguage = await _context.TeacherLanguages
                .Include(t => t.Language)
                .Include(t => t.Level)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherLanguage == null)
            {
                return NotFound();
            }

            return View(teacherLanguage);
        }

        // GET: TeacherLanguage/Create
        public IActionResult Create()
        {
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription");
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress");
            return View();
        }

        // POST: TeacherLanguage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherId,LanguageId,LevelId,Id")] TeacherLanguage teacherLanguage)
        {
            if (ModelState.IsValid)
            {
                teacherLanguage.Id = Guid.NewGuid();
                _context.Add(teacherLanguage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription", teacherLanguage.LanguageId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription", teacherLanguage.LevelId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", teacherLanguage.TeacherId);
            return View(teacherLanguage);
        }

        // GET: TeacherLanguage/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherLanguage = await _context.TeacherLanguages.FindAsync(id);
            if (teacherLanguage == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription", teacherLanguage.LanguageId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription", teacherLanguage.LevelId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", teacherLanguage.TeacherId);
            return View(teacherLanguage);
        }

        // POST: TeacherLanguage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TeacherId,LanguageId,LevelId,Id")] TeacherLanguage teacherLanguage)
        {
            if (id != teacherLanguage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherLanguage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherLanguageExists(teacherLanguage.Id))
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
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription", teacherLanguage.LanguageId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription", teacherLanguage.LevelId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", teacherLanguage.TeacherId);
            return View(teacherLanguage);
        }

        // GET: TeacherLanguage/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherLanguage = await _context.TeacherLanguages
                .Include(t => t.Language)
                .Include(t => t.Level)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherLanguage == null)
            {
                return NotFound();
            }

            return View(teacherLanguage);
        }

        // POST: TeacherLanguage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var teacherLanguage = await _context.TeacherLanguages.FindAsync(id);
            if (teacherLanguage != null)
            {
                _context.TeacherLanguages.Remove(teacherLanguage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherLanguageExists(Guid id)
        {
            return _context.TeacherLanguages.Any(e => e.Id == id);
        }
    }
}
