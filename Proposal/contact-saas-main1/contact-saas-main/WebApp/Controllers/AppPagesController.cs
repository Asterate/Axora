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
[Route("HomeDashboard")]
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

    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var userId = GetCurrentUserId();
        if (!userId.HasValue)
        {
            return Challenge();
        }

        var projectDtos = await _projectService.GetAllAsync(userId.Value);
        var projects = projectDtos.Select(p => new Project { Id = p.Id, ProjectName = p.ProjectName, Funding = p.Funding, Requirements = p.Requirements ?? string.Empty, RequirementsFilePath = p.RequirementsFilePath, ProjectTypeId = p.PublicTypeId }).ToList();
        return View("~/Views/AppPages/HomeDashboard/HomeDashboard.cshtml", projects);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        ViewData["ProjectTypeId"] = new SelectList(_context.ProjectTypes, "Id", "Name");
        return View("~/Views/AppPages/HomeDashboard/Create.cshtml"); // ← correct
    }

    [HttpPost("Create")]
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
            dto.PublicTypeId = dto.PublicTypeId;
            await _projectService.CreateAsync(dto, userId.Value);
            return RedirectToAction(nameof(Index));
        }
        ViewData["ProjectTypeId"] = new SelectList(_context.ProjectTypes, "Id", "Name", dto.PublicTypeId);
        return View("~/Views/AppPages/HomeDashboard/Create.cshtml", dto);
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

[ApiExplorerSettings(IgnoreApi = true)]
[Route("ScheduleDashboard")]
[Authorize(Roles = "admin,employee, owner, instituteadmin, institutemanager")]
public class ScheduleDashboardController : Controller
{
    public IActionResult Index()
    {
        // Schedule page doesn't exist, redirect to home for now
        return RedirectToAction("Index", "HomeDashboard");
    }
}



[ApiExplorerSettings(IgnoreApi = true)]
[Route("AdminDashboard")]
[Authorize(Roles = "admin")]
public class AdminDashboardController : Controller
{
    private readonly AppDbContext _context;

    public AdminDashboardController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var model = new AdminDashboardViewModel
        {
            TotalUsers = await _context.Users.CountAsync(),
            TotalInstitutes = await _context.Institutes
                .Where(i => i.DeletedAt == null)
                .CountAsync(),
            TotalLabs = await _context.Labs
                .Where(l => l.DeletedAt == null)
                .CountAsync(),
            TotalProjects = await _context.Projects
                .CountAsync(),
            
            RecentInstitutes = await _context.Institutes
                .Where(i => i.DeletedAt == null)
                .OrderByDescending(i => i.CreatedAt)
                .Take(5)
                .ToListAsync(),
                
            RecentProjects = await _context.Projects
                .OrderByDescending(p => p.Id)
                .Take(5)
                .ToListAsync()
        };

        return View("~/Views/AppPages/AdminDashboard/AdminDashboard.cshtml", model);
    }
}
