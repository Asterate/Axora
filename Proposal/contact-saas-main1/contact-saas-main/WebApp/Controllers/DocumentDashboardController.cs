using App.DAL.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Roles = "admin, employee, owner, instituteadmin, guest")]
public class DocumentDashboardController : Controller
{
    private readonly DocumentService _document;

    public DocumentDashboardController(DocumentService documentService)
    {
        _document = documentService;
    }

    public IActionResult Index()
    {
        var documents =  _document.FindDeletedAsync().Result;

        var viewModel = new DocumentationViewModel
        {
            Documents = documents,
        };

        return View(viewModel);
    }
}