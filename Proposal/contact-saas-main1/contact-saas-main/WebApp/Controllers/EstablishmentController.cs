using App.DAL.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Roles = "admin")]
[Route("AdminDashboard/Establishments")]
public class EstablishmentsController : Controller
{
    private readonly AppDbContext _context;

    public EstablishmentsController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var institutes = _context.Institutes
            .Where(s => s.DeletedAt == null)
            .ToList();

        var labs = _context.Labs
            .Include(l => l.LabType)
            .Where(s => s.DeletedAt == null)
            .ToList();

        var viewModel = new EstablishmentsViewModel
        {
            Institutes = institutes,
            Labs = labs
        };

        return View("~/Views/AppPages/AdminDashboard/Establishments.cshtml", viewModel);
    }
}