using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using WebApp.ViewModels;
using ScheduleEntity = App.Domain.Entities.Schedule;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Roles = "admin,employee,owner,instituteadmin,institutemanager")]
public class ScheduleDashboardController : Controller
{
    private readonly ScheduleService _scheduleService;

    public ScheduleDashboardController(ScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    public async Task<IActionResult> Index()
    {
        var schedules = await _scheduleService.GetAllAsync();

        var viewModel = new ScheduleDashboardViewModel
        {
            Schedules = schedules
        };

        return View(viewModel);
    }
}
