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
    public class AttendanceRecordController : Controller
    {
        private readonly AppDbContext _context;

        public AttendanceRecordController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AttendanceRecord
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AttendanceRecords.Include(a => a.ConfirmedBy).Include(a => a.Enrollment).Include(a => a.Schedule);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AttendanceRecord/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceRecord = await _context.AttendanceRecords
                .Include(a => a.ConfirmedBy)
                .Include(a => a.Enrollment)
                .Include(a => a.Schedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendanceRecord == null)
            {
                return NotFound();
            }

            return View(attendanceRecord);
        }

        // GET: AttendanceRecord/Create
        public IActionResult Create()
        {
            ViewData["ConfirmedById"] = new SelectList(_context.Teachers, "Id", "TeacherAddress");
            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "EnrollmentSeason");
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "ScheduleEnvironment");
            return View();
        }

        // POST: AttendanceRecord/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentId,ScheduleId,ConfirmedById,AttendanceRecordCreatedAt,AttendanceRecordUpdatedAt,AttendanceRecordDeletedAt,Attendance,Id")] AttendanceRecord attendanceRecord)
        {
            if (ModelState.IsValid)
            {
                attendanceRecord.Id = Guid.NewGuid();
                _context.Add(attendanceRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConfirmedById"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", attendanceRecord.ConfirmedById);
            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "EnrollmentSeason", attendanceRecord.EnrollmentId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "ScheduleEnvironment", attendanceRecord.ScheduleId);
            return View(attendanceRecord);
        }

        // GET: AttendanceRecord/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceRecord = await _context.AttendanceRecords.FindAsync(id);
            if (attendanceRecord == null)
            {
                return NotFound();
            }
            ViewData["ConfirmedById"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", attendanceRecord.ConfirmedById);
            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "EnrollmentSeason", attendanceRecord.EnrollmentId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "ScheduleEnvironment", attendanceRecord.ScheduleId);
            return View(attendanceRecord);
        }

        // POST: AttendanceRecord/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EnrollmentId,ScheduleId,ConfirmedById,AttendanceRecordCreatedAt,AttendanceRecordUpdatedAt,AttendanceRecordDeletedAt,Attendance,Id")] AttendanceRecord attendanceRecord)
        {
            if (id != attendanceRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendanceRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceRecordExists(attendanceRecord.Id))
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
            ViewData["ConfirmedById"] = new SelectList(_context.Teachers, "Id", "TeacherAddress", attendanceRecord.ConfirmedById);
            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "EnrollmentSeason", attendanceRecord.EnrollmentId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "ScheduleEnvironment", attendanceRecord.ScheduleId);
            return View(attendanceRecord);
        }

        // GET: AttendanceRecord/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceRecord = await _context.AttendanceRecords
                .Include(a => a.ConfirmedBy)
                .Include(a => a.Enrollment)
                .Include(a => a.Schedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendanceRecord == null)
            {
                return NotFound();
            }

            return View(attendanceRecord);
        }

        // POST: AttendanceRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var attendanceRecord = await _context.AttendanceRecords.FindAsync(id);
            if (attendanceRecord != null)
            {
                _context.AttendanceRecords.Remove(attendanceRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceRecordExists(Guid id)
        {
            return _context.AttendanceRecords.Any(e => e.Id == id);
        }
    }
}
