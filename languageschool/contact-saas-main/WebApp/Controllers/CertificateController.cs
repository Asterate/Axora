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
    public class CertificateController : Controller
    {
        private readonly AppDbContext _context;

        public CertificateController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Certificate
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Certificates.Include(c => c.Course).Include(c => c.Enrollment).Include(c => c.Language).Include(c => c.Level);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Certificate/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificate = await _context.Certificates
                .Include(c => c.Course)
                .Include(c => c.Enrollment)
                .Include(c => c.Language)
                .Include(c => c.Level)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certificate == null)
            {
                return NotFound();
            }

            return View(certificate);
        }

        // GET: Certificate/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseDescription");
            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "EnrollmentSeason");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription");
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription");
            return View();
        }

        // POST: Certificate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,LanguageId,LevelId,EnrollmentId,CertificateName,CertificateDescription,CertificateGivenOutAt,Id")] Certificate certificate)
        {
            if (ModelState.IsValid)
            {
                certificate.Id = Guid.NewGuid();
                _context.Add(certificate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseDescription", certificate.CourseId);
            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "EnrollmentSeason", certificate.EnrollmentId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription", certificate.LanguageId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription", certificate.LevelId);
            return View(certificate);
        }

        // GET: Certificate/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificate = await _context.Certificates.FindAsync(id);
            if (certificate == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseDescription", certificate.CourseId);
            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "EnrollmentSeason", certificate.EnrollmentId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription", certificate.LanguageId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription", certificate.LevelId);
            return View(certificate);
        }

        // POST: Certificate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CourseId,LanguageId,LevelId,EnrollmentId,CertificateName,CertificateDescription,CertificateGivenOutAt,Id")] Certificate certificate)
        {
            if (id != certificate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certificate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificateExists(certificate.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseDescription", certificate.CourseId);
            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "EnrollmentSeason", certificate.EnrollmentId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageDescription", certificate.LanguageId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelDescription", certificate.LevelId);
            return View(certificate);
        }

        // GET: Certificate/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificate = await _context.Certificates
                .Include(c => c.Course)
                .Include(c => c.Enrollment)
                .Include(c => c.Language)
                .Include(c => c.Level)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certificate == null)
            {
                return NotFound();
            }

            return View(certificate);
        }

        // POST: Certificate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var certificate = await _context.Certificates.FindAsync(id);
            if (certificate != null)
            {
                _context.Certificates.Remove(certificate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificateExists(Guid id)
        {
            return _context.Certificates.Any(e => e.Id == id);
        }
    }
}
