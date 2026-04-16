using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using App.DAL.EF;
using App.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.ViewModels;

namespace WebApp.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context, ILogger<HomeController> logger)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var hasInstitute = await _context.InstituteUsers
                .Include(iu => iu.User)
                .AnyAsync(iu => iu.User.Id.ToString() == userId);

            if (!hasInstitute)
                return RedirectToAction("Index", "InstituteChoice");

            return RedirectToAction("Index", "HomeDashboard");
        }
        return Redirect("/Identity/Account/Login?ReturnUrl=%2F");
    }

    public IActionResult HomeDashboard()
    {
        var projects = _context.Projects.ToList() ?? new List<Project>(); // never null
        return View("Views/AppPages/HomeDashboard/HomeDashboard.cshtml", projects);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        try
        {
            var reqCulture = new RequestCulture(culture);

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(reqCulture),
                new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                }
            );
        }
        catch (Exception e)
        {
            _logger.LogError("SetLanguage exception: {}", e.Message);
        }

        return LocalRedirect(returnUrl);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}