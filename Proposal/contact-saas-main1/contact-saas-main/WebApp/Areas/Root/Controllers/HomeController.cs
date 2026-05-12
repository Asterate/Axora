using App.DAL.EF;
using App.Modules.Identity.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Areas.SysAdmin.Controllers;

[Area("Root")]
[Authorize(Roles = "admin")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IdentityModuleDbContext _context;


    public HomeController(ILogger<HomeController> logger, IdentityModuleDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
  
}