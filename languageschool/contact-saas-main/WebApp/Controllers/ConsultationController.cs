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
    public class ConsultationController : Controller
    {
        private readonly AppDbContext _context;

        public ConsultationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Consultation
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Consultations.Include(c => c.Schedule).Include(c => c.Student).Include(c => c.Teacher);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Consultation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultations
                .Include(c => c.Schedule)
                .Include(c => c.Student)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        // GET: Consultation/Create
        public IActionResult Create()
        {
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "ScheduleEnvironment");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "StudentAddress");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress");
            return View();
        }

        // POST: Consultation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,TeacherId,StudentId,ConsultationName,ConsultationDescription,ConsultationCreatedAt,ConsultationUpdatedAt,ConsultationDeletedAt,Id")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                consultation.Id = Guid.NewGuid();
                _context.Add(consultation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "ScheduleEnvironment", consultation.ScheduleId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "StudentAddress", consultation.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", consultation.TeacherId);
            return View(consultation);
        }

        // GET: Consultation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation == null)
            {
                return NotFound();
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "ScheduleEnvironment", consultation.ScheduleId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "StudentAddress", consultation.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", consultation.TeacherId);
            return View(consultation);
        }

        // POST: Consultation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ScheduleId,TeacherId,StudentId,ConsultationName,ConsultationDescription,ConsultationCreatedAt,ConsultationUpdatedAt,ConsultationDeletedAt,Id")] Consultation consultation)
        {
            if (id != consultation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultationExists(consultation.Id))
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
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "ScheduleEnvironment", consultation.ScheduleId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "StudentAddress", consultation.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", consultation.TeacherId);
            return View(consultation);
        }

        // GET: Consultation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultations
                .Include(c => c.Schedule)
                .Include(c => c.Student)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        // POST: Consultation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation != null)
            {
                _context.Consultations.Remove(consultation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultationExists(Guid id)
        {
            return _context.Consultations.Any(e => e.Id == id);
        }
    }
}
