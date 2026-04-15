using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Route("ProjectDashboard")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ProjectDashboardController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectDashboardController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProjectDashboard
        public IActionResult Index()
        {
            var experiments = _context.Experiments
                .Include(e => e.ExperimentType)
                .Where(e => e.DeletedAt == null)
                .ToList();

            var schedules = _context.Schedules
                .Where(s => s.DeletedAt == null)
                .ToList();

            var viewModel = new ProjectDashboardViewModel
            {
                Experiments = experiments,
                Schedules = schedules
            };

            return View("~/Views/AppPages/ProjectDashboard/ProjectDashboard.cshtml", viewModel);
        }
    }
}