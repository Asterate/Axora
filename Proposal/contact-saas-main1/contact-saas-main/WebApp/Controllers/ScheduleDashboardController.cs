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
    private readonly AppDbContext _context;

    public ScheduleDashboardController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var schedules = await _context.Schedules
            .Include(s => s.ExperimentTask)
                .ThenInclude(t => t.Experiment)
            .Include(s => s.Lab)
            .Where(s => s.DeletedAt == null)
            .ToListAsync();

        var viewModel = new ScheduleDashboardViewModel
        {
            Schedules = schedules
        };

        return View(viewModel);
    }
}

public class ScheduleDashboardViewModel
{
    public IEnumerable<ScheduleEntity> Schedules { get; set; } = new List<ScheduleEntity>();
}
