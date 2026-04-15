using App.DAL.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Roles = "admin")]
[Route("AdminDashboard/LookupData")]
public class LookupDataController : Controller
{
    private readonly AppDbContext _context;

    public LookupDataController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
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