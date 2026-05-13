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
using WebApp.ViewModels;
using InstituteEntity = App.Domain.Entities.Institute;
using Lab = App.Domain.Entities.Lab;
using Project = App.Domain.Entities.Project;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize]
public class HomeDashboardController : Controller
{
    private readonly IProjectService _projectService;
    
    public HomeDashboardController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    // GET: HomeDashboard
    public async Task<IActionResult> Index()
    {
        var userId = GetCurrentUserId();
        if (!userId.HasValue)
        {
            return Challenge();
        }

        var projectDtos = await _projectService.GetAllAsync(userId.Value);
        var projects = projectDtos.Select(p => new Project { Id = p.Id}).ToList();
        return View("HomeDashboard", projects);
    }

    // GET: HomeDashboard/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: HomeDashboard/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProjectRequest dto)
    {
        var userId = GetCurrentUserId();
        if (!userId.HasValue)
        {
            return Challenge();
        }
        
        if (ModelState.IsValid)
        {
            await _projectService.CreateAsync(dto, userId.Value);
            return RedirectToAction(nameof(Index));
        }
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
