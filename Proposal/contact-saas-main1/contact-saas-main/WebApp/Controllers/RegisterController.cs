using App.Domain.Identity;
using App.Modules.Project.Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class RegisterController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly InstituteService _instituteService;
    private readonly InstituteTypeService _instituteTypeService;
    private readonly ILogger<RegisterController> _logger;

    public RegisterController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        InstituteService instituteService,
        InstituteTypeService instituteTypeService,
        ILogger<RegisterController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _instituteService = instituteService;
        _instituteTypeService = instituteTypeService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new RegisterViewModel
        {
            Institutes = await _instituteService.GetActivesAsync(),
            InstituteTypes = await _instituteTypeService.GetActivesAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdowns(model);
            return View(model);
        }

        var appUser = new AppUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(appUser, model.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            await LoadDropdowns(model);
            return View(model);
        }

        _logger.LogInformation("User {Email} registered successfully", appUser.Email);

        // Cookie auth for MVC — no JWT needed here
        await _signInManager.SignInAsync(appUser, isPersistent: false);

        return RedirectToAction("Index", "InstituteChoice");
    }

    private async Task LoadDropdowns(RegisterViewModel model)
    {
        model.Institutes = await _instituteService.GetActivesAsync();
        model.InstituteTypes = await _instituteTypeService.GetActivesAsync();
    }
}