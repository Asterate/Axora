using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.BLL.Services;
using App.DAL.EF;
using App.Domain.Entities;
using App.DTO.v1;
using App.Modules.Research;
using App.Shared.Contracts;
using WebApp.ViewModels;
using InstituteEntity = App.Domain.Entities.Institute;
using Lab = App.Domain.Entities.Lab;
using Project = App.Domain.Entities.Project;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize]
public class HomeDashboardController : Controller
{
    private readonly ProjectService _projectService;
    private readonly ProjectTypeService _projectTypeService;
    
    public HomeDashboardController(ProjectService projectService, ProjectTypeService projectTypeService)
    {
        _projectService = projectService;
        _projectTypeService = projectTypeService;
    }

    // GET: HomeDashboard
    public async Task<IActionResult> Index()
    {
        var userId = GetCurrentUserId();
        if (!userId.HasValue)
        {
            return Challenge();
        }

        var projectDtos = await _projectService.GetAllAsync();
        var projects = projectDtos.Select(p => new Project { Id = p.Id}).ToList();
        return View("HomeDashboard", projects);
    }

    // GET: HomeDashboard/Create
    public async Task<IActionResult> Create()
    {
        var model = new HomeDashboardViewModel
        {
            ProjectTypes = await _projectTypeService.GetActivesAsync()
        };
        return View(model);
    }

    // POST: HomeDashboard/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HomeDashboardViewModel dto)
    {
        var userId = GetCurrentUserId();
        if (!userId.HasValue) return Challenge();
    
        if (ModelState.IsValid)
        {
            var request = new CreateProjectRequest
            {
                ProjectName = dto.ProjectName,
                Funding = dto.Funding,
                Requirements = dto.Requirements,
                RequirementsFilePath = dto.RequirementsFilePath,
                ProjectTypeId = dto.ProjectTypeId
            };
            await _projectService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }

        dto.ProjectTypes = await _projectTypeService.GetActivesAsync();
        return View(dto);
    }

    private Guid? GetCurrentUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return null;
        }
        return userId;
    }
}
