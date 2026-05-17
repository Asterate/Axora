using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Lab.Application.Services;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Roles = "admin")]
[Route("AdminDashboard/Establishments")]
[Route("Establishments")]
public class EstablishmentsController : Controller
{
    private readonly IInstituteService _institute;
    private readonly ILabService _lab;

    public EstablishmentsController(IInstituteService instituteService, ILabService labService)
    {
        _institute = instituteService;
        _lab = labService;
    }

    public IActionResult Index()
    {
        var institutes = _institute.FindDeletedAsync().Result;


        var labs = _lab.GetAllAsync().Result;

        var viewModel = new EstablishmentsViewModel
        {
            Institutes = institutes,
            Labs = labs
        };

        return View(viewModel);
    }
}