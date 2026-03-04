using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Owner.Controllers;

[Area("Owner")]
[Authorize]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Owner Dashboard";
        return View();
    }
}
