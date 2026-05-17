using System.Security.Claims;
using App.Modules.Project.Application.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "admin, employee, owner, instituteadmin")]
    public class ProjectDashboardController : Controller
    {
        private readonly IExperimentService _experimentService;
        private readonly IScheduleService _scheduleService;
        private readonly IProjectService _projectService;
        private readonly IExperimentTypeService _experimentTypeService;

        public ProjectDashboardController(IExperimentService experimentService, IScheduleService scheduleService, 
            IProjectService projectService, IExperimentTypeService experimentTypeService)
        {
            _experimentService = experimentService;
            _scheduleService = scheduleService;
            _projectService = projectService;
            _experimentTypeService = experimentTypeService;
        }

        // GET: ProjectDashboard
        public async Task<IActionResult> Index()
        {
            var model = await ProjectDashboardViewModel.CreateInitials(_scheduleService, _experimentTypeService);
            return View("Index", model);
        }
        // GET: Experiment/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var instituteIdClaim = User.FindFirstValue("InstituteId");
            if (instituteIdClaim == null) return NotFound();
            var instituteId = Guid.Parse(instituteIdClaim);
            var experiment = await _experimentService.GetByIdAsync(id, instituteId);
            if (experiment == null) return NotFound();
            return View(experiment);
        }
        
        // GET: Experiment/Create
        public async Task<IActionResult> Create()
            => View(await ProjectDashboardViewModel.ForCreate(_experimentTypeService, _projectService));
        // POST: HomeDashboard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectDashboardViewModel dto)
        {
            if (ModelState.IsValid)
            {
                var instituteIdClaim = User.FindFirstValue("InstituteId");
                if (instituteIdClaim == null) return NotFound();
                var instituteId = Guid.Parse(instituteIdClaim);
                await _experimentService.CreateAsync(dto.ExperimentRequest, instituteId);
                return RedirectToAction(nameof(Index));
            }
            dto.ExperimentTypes = await _experimentTypeService.GetActivesAsync();
            dto.Projects = await _projectService.GetActivesAsync();
            return View("Index", dto);
        }

        // GET: Experiment/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var instituteIdClaim = User.FindFirstValue("InstituteId");
            if (instituteIdClaim == null) return NotFound();
            var instituteId = Guid.Parse(instituteIdClaim);
            var experiment = await _experimentService.GetByIdEditAsync(id,  instituteId);
            if (experiment == null) return NotFound();
            var model = await ProjectDashboardViewModel.ForEdit(experiment, _experimentTypeService, _projectService);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProjectDashboardViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var instituteIdClaim = User.FindFirstValue("InstituteId");
            if (instituteIdClaim == null) return NotFound();
            var instituteId = Guid.Parse(instituteIdClaim);
            await _experimentService.UpdateAsync(id, model.ExperimentRequest,  instituteId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Experiment/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var instituteIdClaim = User.FindFirstValue("InstituteId");
            if (instituteIdClaim == null) return NotFound();
            var instituteId = Guid.Parse(instituteIdClaim);
            var response = await _experimentService.GetByIdAsync(id,  instituteId);
            if (response == null) return NotFound();
    
            return View(response);
        }

        // POST: Experiment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var instituteIdClaim = User.FindFirstValue("InstituteId");
            if (instituteIdClaim == null) return NotFound();
            var instituteId = Guid.Parse(instituteIdClaim);
            await _experimentService.DeleteAsync(id, instituteId);
            return RedirectToAction(nameof(Index));
        }
    }
    
}