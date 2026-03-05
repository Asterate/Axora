using App.BLL;
using App.DAL.EF;
using App.Domain.Entities;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class SchedulePageController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public SchedulePageController(AppDbContext context, UserManager<AppUser> usermanager)
    {
        _context = context;
        _userManager = usermanager;
    }

    public async Task<IActionResult> CreateSchedule(Guid courseId)
    {
        var course = await _context.Courses
            .Include(c => c.Teacher)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        if (course == null) return NotFound();

        var vm = new CreateScheduleViewModel
        {
            CourseId = courseId,
            CourseName = course.CourseName,
            TeacherId = course.TeacherId
        };

        await LoadSelectLists(vm);
        return View("CreateSchedule", vm);
    }

        public async Task<IActionResult> EditSchedule(Guid id)
        {
            var schedule = await ScheduleService.GetScheduleById(_context, id);
            if (schedule == null) return NotFound();

            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.TeacherId == schedule.Availability!.TeacherId);

        if (course == null) return NotFound();

        var vm = new CreateScheduleViewModel
        {
            ScheduleId = id,
            CourseId = course.Id,
            CourseName = course.CourseName,
            TeacherId = course.TeacherId,
            AvailabilityId = schedule.AvailabilityId,
            EScheduleType = schedule.EScheduleType,
            ScheduleStartAt = schedule.ScheduleStartAt,
            ScheduleEndAt = schedule.ScheduleEndAt,
            ScheduleEnvironment = schedule.ScheduleEnvironment
        };

        await LoadSelectLists(vm);
        return View("CreateSchedule", vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSchedule(CreateScheduleViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            await LoadSelectLists(vm);
            return View("CreateSchedule", vm);
        }

        if (vm.ScheduleId.HasValue)
        {
            var existingSchedule = await ScheduleService.GetScheduleById(_context, vm.ScheduleId.Value);
            if (existingSchedule != null)
            {
                existingSchedule.AvailabilityId = vm.AvailabilityId;
                existingSchedule.EScheduleType = vm.EScheduleType;
                existingSchedule.ScheduleStartAt = vm.ScheduleStartAt;
                existingSchedule.ScheduleEndAt = vm.ScheduleEndAt;
                existingSchedule.ScheduleEnvironment = vm.ScheduleEnvironment;
                await ScheduleService.UpdateSchedule(_context, existingSchedule);
            }
        }
        else
        {
            var newSchedule = new Schedule
            {
                AvailabilityId = vm.AvailabilityId,
                EScheduleType = vm.EScheduleType,
                ScheduleStartAt = vm.ScheduleStartAt,
                ScheduleEndAt = vm.ScheduleEndAt,
                ScheduleEnvironment = vm.ScheduleEnvironment
            };
            await ScheduleService.AddSchedule(_context, newSchedule);
        }

        return RedirectToAction("CourseDesktop", "CoursePage", new { id = vm.CourseId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "COMPANYMANAGER,COMPANYOWNER,admin,TEACHER")]
    public async Task<IActionResult> DeleteSchedule(Guid id, Guid courseId, Guid companyId)
    {    
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null) 
            return RedirectToAction("CourseDesktop", "CoursePage", new { id = courseId });

        var userGuid = currentUser.Id;
        var companyUser = await _context.CompanyUsers
            .FirstOrDefaultAsync(cu => cu.AppUserId == userGuid && cu.CompanyId == companyId);
        if (companyUser == null)
        {
            return Forbid();
        }
        
        await ScheduleService.DeleteSchedule(_context, id);
        return RedirectToAction("CourseDesktop", "CoursePage", new { id = courseId });
    }

    private async Task LoadSelectLists(CreateScheduleViewModel vm)
    {
        if (vm.TeacherId.HasValue)
        {
            var availabilities = await ScheduleService.GetAvailabilitiesByTeacherId(_context, vm.TeacherId.Value);
            vm.Availabilities = availabilities.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = $"Availability: {a.AvailabilityStartDate.ToString("MMM dd, yyyy")} - {a.AvailabilityEndDate.ToString("MMM dd, yyyy")}"
            }).ToList();
        }

        vm.ScheduleTypes = Enum.GetValues(typeof(EScheduleType))
            .Cast<EScheduleType>()
            .Select(t => new SelectListItem
            {
                Value = t.ToString(),
                Text = t.ToString()
            }).ToList();
    }
}