using App.Modules.Project.Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Roles = "admin, employee, owner, instituteadmin, guest")]
public class AnalysisDashboardController : Controller
{
    private readonly IResultService _result;

    public AnalysisDashboardController(IResultService resultService)
    {
        _result = resultService;
    }

    public async Task<IActionResult> Index()
    {
        var results = await _result.GetAllAsync();

        var viewModel = new AnalysisDashboardViewModel
        {
            Results = results,
        };

        return View(viewModel); 
    }
}