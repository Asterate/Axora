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
    public class ScheduleController : Controller
    {
        private readonly AppDbContext _context;

        public ScheduleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Schedule
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Schedules.Include(s => s.Equipment).Include(s => s.ExperimentTask).Include(s => s.InstituteUser).Include(s => s.Lab);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Schedule/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Equipment)
                .Include(s => s.ExperimentTask)
                .Include(s => s.InstituteUser)
                .Include(s => s.Lab)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedule/Create
        public IActionResult Create()
        {
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "EquipmentName");
            ViewData["ExperimentTaskId"] = new SelectList(_context.ExperimentTasks, "Id", "TaskName");
            ViewData["InstituteUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id");
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress");
            return View();
        }

        // POST: Schedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleName,ScheduleDescription,ColorCode,Status,CreatedAt,UpdatedAt,DeletedAt,StartTime,EndTime,LabId,InstituteUserId,EquipmentId,ExperimentTaskId,Id")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                schedule.Id = Guid.NewGuid();
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "EquipmentName", schedule.EquipmentId);
            ViewData["ExperimentTaskId"] = new SelectList(_context.ExperimentTasks, "Id", "TaskName", schedule.ExperimentTaskId);
            ViewData["InstituteUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id", schedule.InstituteUserId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress", schedule.LabId);
            return View(schedule);
        }

        // GET: Schedule/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "EquipmentName", schedule.EquipmentId);
            ViewData["ExperimentTaskId"] = new SelectList(_context.ExperimentTasks, "Id", "TaskName", schedule.ExperimentTaskId);
            ViewData["InstituteUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id", schedule.InstituteUserId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress", schedule.LabId);
            return View(schedule);
        }

        // POST: Schedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ScheduleName,ScheduleDescription,ColorCode,Status,CreatedAt,UpdatedAt,DeletedAt,StartTime,EndTime,LabId,InstituteUserId,EquipmentId,ExperimentTaskId,Id")] Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
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
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "EquipmentName", schedule.EquipmentId);
            ViewData["ExperimentTaskId"] = new SelectList(_context.ExperimentTasks, "Id", "TaskName", schedule.ExperimentTaskId);
            ViewData["InstituteUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id", schedule.InstituteUserId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress", schedule.LabId);
            return View(schedule);
        }

        // GET: Schedule/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Equipment)
                .Include(s => s.ExperimentTask)
                .Include(s => s.InstituteUser)
                .Include(s => s.Lab)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(Guid id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }
}
