using System.Diagnostics;
using System.Security.Claims;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;
using WebApp.ViewModels;

namespace WebApp.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
[AllowAnonymous]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly ProjectService _projectService;
    private readonly InstituteUserService _instituteUserService;
    public HomeController(ILogger<HomeController> logger , UserManager<AppUser> userManager, ProjectService projectService,
        InstituteUserService instituteUserService)
    {
        _logger = logger;
        _userManager = userManager;
        _projectService = projectService;
        _instituteUserService = instituteUserService;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = User.UserId();
            var hasInstitute = await _instituteUserService.HasInstituteAsync(userId);
            var user = await _userManager.FindByIdAsync(userName!);
            if (user != null)
            {
                user.LastSeen = DateTimeOffset.UtcNow;
                await _userManager.UpdateAsync(user);
            }

            // if (!hasInstitute) return RedirectToAction("Index", "InstituteChoice");

            return RedirectToAction("Index", "HomeDashboard");
        }
        return Redirect("/Identity/Account/Login?ReturnUrl=%2F");
    }


    public async Task<IActionResult> HomeDashboard()
    {
        var projects = await _projectService.GetAllAsync(); // never null
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