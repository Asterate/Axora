using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Modules.Project.Application.Services;
using App.Modules.Project.Domain;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Roles = "admin,employee,owner,instituteadmin,institutemanager")]
public class ScheduleDashboardController : Controller
{
    private readonly IScheduleService _scheduleService;
    private readonly ILabService _labService;
    private readonly IExperimentService _experimentService;
    private readonly IEquipmentService _equipmentService;

    public ScheduleDashboardController(IScheduleService scheduleService,
        ILabService labService, IExperimentService experimentService, 
        IEquipmentService equipmentService)
    {
        _scheduleService = scheduleService;
        _labService = labService;
        _experimentService = experimentService;
        _equipmentService = equipmentService;
    }

    public async Task<IActionResult> Index()
    {
        var projects = await _scheduleService.GetAllAsync();
        return View("Index", projects);
    }
       // GET: Schedule/Details/5
       public async Task<IActionResult> Details(Guid id)
       {
           var item = await _scheduleService.GetByIdAsync(id);
           if (item == null) return NotFound();
           return View(item);
       }

        // GET: Schedule/Create
        public async Task<IActionResult> Create()
            => View(await ScheduleDashboardViewModel.ForCreate(_labService, _experimentService, _equipmentService));

        // POST: Schedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScheduleDashboardViewModel dto)
        {
            if (ModelState.IsValid)
            {
                await _scheduleService.CreateAsync(dto.ScheduleRequest);
                return RedirectToAction(nameof(Index));
            }
            dto.Labs = await _labService.GetActivesAsync();
            dto.Equipments = await _equipmentService.GetActivesAsync();
            dto.Experiments = await _experimentService.GetActivesAsync();
            return View(dto);
        }

        // GET: Schedule/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var item = await _scheduleService.GetByIdEditAsync(id);
            if (item == null) return NotFound();
            var model = await ScheduleDashboardViewModel.ForEdit(item, _labService, _experimentService, _equipmentService);
            return View(model);
        }

        // POST: Schedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ScheduleDashboardViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _scheduleService.UpdateAsync(id, model.ScheduleRequest);
            return RedirectToAction(nameof(Index));
        }

        // GET: Schedule/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var project = await _scheduleService.GetByIdAsync(id);
            if (project == null) return NotFound();
    
            return View(project);
        }

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _scheduleService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    
}
