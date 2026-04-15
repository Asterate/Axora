using App.DAL.EF;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;
[Route("DocumentationDashboard")]
[ApiExplorerSettings(IgnoreApi = true)]
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

            return View("~/Views/AppPages/DocumentationDashboard/DocumentationDashboard.cshtml", viewModel);
        }
}