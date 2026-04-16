using App.DAL.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Roles = "admin, employee, owner, instituteadmin, guest")]
public class DocumentDashboardController : Controller
{
    private readonly AppDbContext _context;

    public DocumentDashboardController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var documents = _context.Documents
            .Where(s => s.DeletedAt == null)
            .ToList();

        var viewModel = new DocumentationViewModel
        {
            Documents = documents,
        };

        return View(viewModel);
    }
}