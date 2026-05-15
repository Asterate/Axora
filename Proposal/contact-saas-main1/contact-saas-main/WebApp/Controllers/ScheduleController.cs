using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Entities;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Mappers;
using App.Modules.Project.Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class ScheduleController : Controller
    {
        private readonly ScheduleService _scheduleService;

        public ScheduleController(ScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        // GET: Schedule
        public async Task<IActionResult> Index()
        {
            var schedules = _scheduleService.GetAllAsync();
            return View(schedules);
        }

        // GET: Schedule/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _scheduleService.GetByIdAsync(id.Value);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedule/Create
        public IActionResult Create()
        {
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
                var create = new CreateScheduleRequest
                {
                    ScheduleName =  schedule.ScheduleName ?? "??",
                    Status = schedule.Status,
                    ColorCode =  schedule.ColorCode,
                    CreatedAt = schedule.CreatedAt,
                    LabId = schedule.LabId,
                    InstituteUserId = schedule.InstituteUserId,
                    EquipmentId =  schedule.EquipmentId,
                };
                await _scheduleService.CreateAsync(create);
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        // GET: Schedule/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _scheduleService.GetByIdAsync(id.Value);
            if (schedule == null)
            {
                return NotFound();
            }
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
                    var update = ScheduleMapper.ToUpdateRequest(schedule);
                    await _scheduleService.UpdateAsync(id, update);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ScheduleExists(schedule.Id))
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
            return View(schedule);
        }

        // GET: Schedule/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _scheduleService.GetByIdAsync(id.Value);
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
            var schedule = await _scheduleService.GetByIdAsync(id);
            if (schedule != null)
            {
                await _scheduleService.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ScheduleExists(Guid id)
        {
            return await _scheduleService.GetByIdAsync(id) != null;
        }
    }
}
