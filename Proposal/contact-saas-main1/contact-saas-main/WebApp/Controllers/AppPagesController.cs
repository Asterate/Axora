using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using WebApp.ViewModels;

namespace WebApp.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
[Route("HomeDashboard")]
[Authorize]
public class HomeDashboardController : Controller
{
    public IActionResult Index()
    {
        return View("~/Views/AppPages/HomeDashboard/HomeDashboard.cshtml");
    }
}

[ApiExplorerSettings(IgnoreApi = true)]
[Route("ScheduleDashboard")]
public class ScheduleDashboardController : Controller
{
    public IActionResult Index()
    {
        // Schedule page doesn't exist, redirect to home for now
        return RedirectToAction("Index", "HomeDashboard");
    }
}



[ApiExplorerSettings(IgnoreApi = true)]
[Route("AdminDashboard")]
public class AdminDashboardController : Controller
{
    private readonly AppDbContext _context;

    public AdminDashboardController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View("~/Views/AppPages/AdminDashboard/AdminDashboard.cshtml");
    }
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("LookupData")]
    public async Task<IActionResult> LookupData()
    {
        var model = new LookupDataViewModel
        {
            CertificationTypes = await _context.CertificationTypes.ToListAsync(),
            DocumentTypes = await _context.DocumentTypes.ToListAsync(),
            EquipmentTypes = await _context.EquipmentTypes.ToListAsync(),
            ExperimentTypes = await _context.ExperimentTypes.ToListAsync(),
            InstituteTypes = await _context.InstituteTypes.ToListAsync(),
            LabTypes = await _context.LabTypes.ToListAsync(),
            ProjectTypes = await _context.ProjectTypes.ToListAsync(),
            ReagentTypes = await _context.ReagentTypes.ToListAsync(),
            TaskTypes = await _context.TaskTypes.ToListAsync()
        };
        
        return View("~/Views/AppPages/AdminDashboard/LookupData.cshtml", model);
    }
}
