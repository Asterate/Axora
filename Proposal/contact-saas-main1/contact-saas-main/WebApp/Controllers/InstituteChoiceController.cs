using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;
using App.DAL.EF;
using App.Domain.Entities;
using App.Domain.Identity;
using App.Helpers;
using App.Modules.Identity.Application.DTO;
using App.Modules.Identity.Application.Interfaces;
using App.Modules.Identity.Application.Services;
using App.Modules.Identity.Domain;
using App.Modules.Identity.Helper;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Services;
using App.Shared.Contracts;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]

public class InstituteChoiceController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IInstituteService _instituteService;
    private readonly IInstituteUserService _instituteUserService;
    private readonly IInstituteTypeService _instituteTypeService;

    public InstituteChoiceController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IInstituteService instituteService,
        IInstituteUserService instituteUserService,
        IInstituteTypeService instituteTypeService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _instituteService = instituteService;
        _instituteUserService = instituteUserService;
        _instituteTypeService = instituteTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new InstituteChoiceViewModel
        {
            Institutes = await _instituteService.GetActivesAsync(),
            InstituteTypes = await _instituteTypeService.GetActivesAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(InstituteChoiceViewModel model)
    {
        // Server-side validation for selecting existing institute
        if (model.InstituteSelection == 0 && model.InstituteId == null)
        {
            ModelState.AddModelError("InstituteId", "Please select an institute");
        }

        if (!ModelState.IsValid)
        {
            await LoadDropdowns(model);
            return View(model);
        }

        try
        {
            // Get current user
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                ModelState.AddModelError(string.Empty, "User not authenticated");
                await LoadDropdowns(model);
                return View(model);
            }

            // Handle selecting existing institute
            if (model.InstituteSelection == 0 && model.InstituteId.HasValue)
            {
                var institute = await _instituteService.GetEntityByIdAsync(model.InstituteId.Value);
                if (institute == null || !institute.Active || institute.DeletedAt != null)
                {
                    ModelState.AddModelError("InstituteId", "Institute not found or inactive");
                    await LoadDropdowns(model);
                    return View(model);
                }

                // Check if user already has an institute user record
                var existingUser = await _instituteUserService.GetByIdAsync(userId);

                if (existingUser != null)
                {
                    // Update existing user's institute
                    existingUser.Id = model.InstituteId.Value;
                }
                else
                {
                    // Create new institute user record
                    var newInstituteUser = new SaveInstituteUserRequest
                    {
                        Id = userId,
                        UserId = userId,
                        Role = EInstituteUserRole.Employee
                    };
                    await _instituteUserService.CreateAsync(newInstituteUser);
                }


                // Sync roles to Identity
                var appUser = await _userManager.FindByIdAsync(userId.ToString());
          
                if (appUser != null)
                {
                    var userInstitute = await _instituteUserService.GetByIdAsync(userId);
                    if (userInstitute != null)
                    {
                        await UserRoleHelper.SyncCompanyUserRolesToIdentityAsync(_userManager, appUser, userInstitute.Role);
                        
                        // Re-sign in to update claims with new roles
                        await _signInManager.RefreshSignInAsync(appUser);
                    }
                }

                return RedirectToAction("Index", "HomeDashboard");
            }

            // Handle creating new institute
            if (model.InstituteSelection == 1)
            {
                // Create new institute
                var newInstitute = new SaveInstituteRequest
                {
                    InstituteName = model.InstituteName ?? "",
                    InstituteCountry = model.InstituteCountry ?? "",
                    InstituteAddress = model.InstituteAddress ?? "",
                    InstitutePhoneNumber = model.InstitutePhoneNumber ?? "",
                    InstituteTypeId = model.InstituteTypeId ?? Guid.Empty,
                    Active = true,
                    CreatedAt = DateTime.UtcNow
                };

                var createdInstitute = await _instituteService.CreateAsync(newInstitute);

                // Create institute user record
                var newInstituteUser = new SaveInstituteUserRequest
                {
                    UserId = userId,
                    InstituteId = createdInstitute.Id,
                    Role = EInstituteUserRole.Owner
                };
                await _instituteUserService.CreateAsync(newInstituteUser);

                // Sync roles to Identity
                var appUser = await _userManager.FindByIdAsync(userId.ToString());
                if (appUser != null)
                {
                    await UserRoleHelper.SyncCompanyUserRolesToIdentityAsync(_userManager, appUser, newInstituteUser.Role);
                }

                return RedirectToAction("Index", "HomeDashboard");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
            ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
            return View(model);
        }

        await LoadDropdowns(model);
        return View(model);
    }

    private async Task LoadDropdowns(InstituteChoiceViewModel model)
    {
        // Load institutes directly from database
        model.Institutes = await _instituteService.GetActivesAsync();

        // Load institute types directly from database
        var types = await _instituteTypeService.GetAllAsync();
        model.InstituteTypes = await _instituteTypeService.GetActivesAsync();
    }
}