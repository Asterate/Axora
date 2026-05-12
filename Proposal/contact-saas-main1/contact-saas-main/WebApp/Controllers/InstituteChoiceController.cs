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
using App.Shared.Contracts;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]

public class InstituteChoiceController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly InstituteService _instituteService;
    private readonly InstituteUserService _instituteUserService;
    private readonly InstituteTypeService _instituteTypeService;

    public InstituteChoiceController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        InstituteService instituteService,
        InstituteUserService instituteUserService,
        InstituteTypeService instituteTypeService)
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
        var model = new InstituteChoiceViewModel();
        
        // Load institutes directly from database
        var institutes = await _instituteService.GetActivesAsync();
        model.Institutes = institutes;

        // Load institute types directly from database
        var types = await _instituteTypeService.GetAllAsync();
        model.InstituteTypes = await _instituteTypeService.GetActivesAsync();

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
                    var newInstituteUser = new CreateInstituteUserRequest
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
                var newInstitute = new CreateInstituteRequest
                {
                    Id =  userId,
                    InstituteName = model.InstituteName ?? "",
                    InstituteCountry = model.InstituteCountry ?? "",
                    InstituteAddress = model.InstituteAddress ?? "",
                    InstitutePhoneNumber = model.InstitutePhoneNumber ?? "",
                    InstituteTypeId = model.InstituteTypeId ?? Guid.Empty,
                    Active = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _instituteService.CreateAsync(newInstitute);

                // Create institute user record
                var newInstituteUser = new CreateInstituteUserRequest
                {
                    UserId = userId,
                    InstituteId = newInstitute.Id,
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
            ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
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