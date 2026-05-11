using App.DAL.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Roles = "admin")]
[Route("AdminDashboard/Establishments")]
[Route("Establishments")]
public class EstablishmentsController : Controller
{
    private readonly InstituteService _institute;
    private readonly LabService _lab;

    public EstablishmentsController(InstituteService instituteService, LabService labService)
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