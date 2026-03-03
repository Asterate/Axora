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
    public class TeacherCertificateController : Controller
    {
        private readonly AppDbContext _context;

        public TeacherCertificateController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TeacherCertificate
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TeacherCertificates.Include(t => t.Language).Include(t => t.Level).Include(t => t.Teacher);
            return View(await appDbContext.ToListAsync());
        }

        // GET: TeacherCertificate/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherCertificate = await _context.TeacherCertificates
                .Include(t => t.Language)
                .Include(t => t.Level)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherCertificate == null)
            {
                return NotFound();
            }

            return View(teacherCertificate);
        }

        // GET: TeacherCertificate/Create
        public IActionResult Create()
        {
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription");
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress");
            return View();
        }

        // POST: TeacherCertificate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherId,LanguageId,LevelId,TeacherCertificateGivenOut,TeacherCertificateName,TeacherCertificateDescription,Id")] TeacherCertificate teacherCertificate)
        {
            if (ModelState.IsValid)
            {
                teacherCertificate.Id = Guid.NewGuid();
                _context.Add(teacherCertificate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription", teacherCertificate.LanguageId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription", teacherCertificate.LevelId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", teacherCertificate.TeacherId);
            return View(teacherCertificate);
        }

        // GET: TeacherCertificate/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherCertificate = await _context.TeacherCertificates.FindAsync(id);
            if (teacherCertificate == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription", teacherCertificate.LanguageId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription", teacherCertificate.LevelId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", teacherCertificate.TeacherId);
            return View(teacherCertificate);
        }

        // POST: TeacherCertificate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TeacherId,LanguageId,LevelId,TeacherCertificateGivenOut,TeacherCertificateName,TeacherCertificateDescription,Id")] TeacherCertificate teacherCertificate)
        {
            if (id != teacherCertificate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherCertificate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherCertificateExists(teacherCertificate.Id))
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
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription", teacherCertificate.LanguageId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription", teacherCertificate.LevelId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", teacherCertificate.TeacherId);
            return View(teacherCertificate);
        }

        // GET: TeacherCertificate/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherCertificate = await _context.TeacherCertificates
                .Include(t => t.Language)
                .Include(t => t.Level)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherCertificate == null)
            {
                return NotFound();
            }

            return View(teacherCertificate);
        }

        // POST: TeacherCertificate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var teacherCertificate = await _context.TeacherCertificates.FindAsync(id);
            if (teacherCertificate != null)
            {
                _context.TeacherCertificates.Remove(teacherCertificate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherCertificateExists(Guid id)
        {
            return _context.TeacherCertificates.Any(e => e.Id == id);
        }
    }
}
