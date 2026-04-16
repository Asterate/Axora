using App.DAL.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Roles = "admin, employee, owner, instituteadmin, guest")]
public class AnalysisDashboardController : Controller
{
    private readonly AppDbContext _context;

    public AnalysisDashboardController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var results = _context.Results
            .Where(e => e.DeletedAt == null)
            .ToList();

        var viewModel = new AnalysisDashboardViewModel
        {
            Results = results,
        };

        return View(viewModel); 
    }
}