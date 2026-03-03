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
    public class SessionController : Controller
    {
        private readonly AppDbContext _context;

        public SessionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Session
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Sessions.Include(s => s.Course).Include(s => s.Schedule).Include(s => s.Teacher);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Session/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Course)
                .Include(s => s.Schedule)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // GET: Session/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseDescription");
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "ScheduleEnvironment");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress");
            return View();
        }

        // POST: Session/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,TeacherId,CourseId,SessionCreatedAt,SessionUpdatedAt,SessionDeletedAt,Id")] Session session)
        {
            if (ModelState.IsValid)
            {
                session.Id = Guid.NewGuid();
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseDescription", session.CourseId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "ScheduleEnvironment", session.ScheduleId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", session.TeacherId);
            return View(session);
        }

        // GET: Session/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseDescription", session.CourseId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "ScheduleEnvironment", session.ScheduleId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", session.TeacherId);
            return View(session);
        }

        // POST: Session/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ScheduleId,TeacherId,CourseId,SessionCreatedAt,SessionUpdatedAt,SessionDeletedAt,Id")] Session session)
        {
            if (id != session.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseDescription", session.CourseId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "ScheduleEnvironment", session.ScheduleId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", session.TeacherId);
            return View(session);
        }

        // GET: Session/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Course)
                .Include(s => s.Schedule)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Session/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(Guid id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}
