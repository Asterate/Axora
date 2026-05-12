using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "admin, employee, owner, instituteadmin")]
    public class ProjectDashboardController : Controller
    {
        private readonly ExperimentService _experimentService;
        private readonly ScheduleService _scheduleService;

        public ProjectDashboardController(ExperimentService experimentService, ScheduleService scheduleService)
        {
            _experimentService = experimentService;
            _scheduleService = scheduleService;
        }

        // GET: ProjectDashboard
        public async Task<IActionResult> Index()
        {
            var experiments = await _experimentService.GetAllAsync();

            var schedules = await _scheduleService.GetAllAsync();

            var viewModel = new ProjectDashboardViewModel
            {
                Experiments = experiments,
                Schedules = schedules
            };

            return View(viewModel);
        }
    }
}