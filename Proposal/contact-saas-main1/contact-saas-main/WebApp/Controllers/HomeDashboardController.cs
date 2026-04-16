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
using WebApp.ViewModels;
using InstituteEntity = App.Domain.Entities.Institute;
using Lab = App.Domain.Entities.Lab;
using Project = App.Domain.Entities.Project;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize]
public class HomeDashboardController : Controller
{
    private readonly AppDbContext _context;
    private readonly IProjectService _projectService;
    
    public HomeDashboardController(AppDbContext context, IProjectService projectService)
    {
        _context = context;
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
        var projects = projectDtos.Select(p => new Project { Id = p.Id, ProjectName = p.ProjectName, Funding = p.Funding, Requirements = p.Requirements ?? string.Empty, RequirementsFilePath = p.RequirementsFilePath, ProjectTypeId = p.ProjectTypeId }).ToList();
        return View("HomeDashboard", projects);
    }

    // GET: HomeDashboard/Create
    public IActionResult Create()
    {
        ViewData["ProjectTypeId"] = new SelectList(_context.ProjectTypes, "Id", "Name");
        return View();
    }

    // POST: HomeDashboard/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProjectDto dto)
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
        ViewData["ProjectTypeId"] = new SelectList(_context.ProjectTypes, "Id", "Name", dto.ProjectTypeId);
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
