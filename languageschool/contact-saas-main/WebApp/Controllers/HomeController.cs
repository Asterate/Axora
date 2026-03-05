using System;
using System.Diagnostics;
using System.Threading.Tasks;
using App.DAL.EF;
using App.Domain.Entities;
using App.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private static int _counter = 0;

    public HomeController(AppDbContext context, ILogger<HomeController> logger, UserManager<AppUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new HomeViewModel
        {
            IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
            UserName = User.Identity?.Name
        };

        if (viewModel.IsAuthenticated)
        {
            // Load public courses
            viewModel.PublicCourses = await _context.Courses
                .Include(c => c.Language)
                .Include(c => c.Level)
                .Include(c => c.Teacher)
                .Include(c => c.Company)
                .Where(c => c.CourseName != null) // Filter out any invalid courses
                .Take(10) // Limit to 10 courses for home page
                .ToListAsync();
        }

        return View(viewModel);
    }

    public async Task<string> HtmxClicked()
    {
        _counter++;
        return "Htmx Click Me - " + _counter;
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