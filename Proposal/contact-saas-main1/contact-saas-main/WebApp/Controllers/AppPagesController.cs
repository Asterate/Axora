using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using WebApp.ViewModels;
using InstituteEntity = App.Domain.Entities.Institute;
using Lab = App.Domain.Entities.Lab;
using ProjectEntity = App.Domain.Entities.Project;

namespace WebApp.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
[Route("HomeDashboard")]
[Authorize]
public class HomeDashboardController : Controller
{
    private readonly AppDbContext _context;
    
    public HomeDashboardController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var projects = _context.Projects.ToList() ?? new List<ProjectEntity>();
        return View("~/Views/AppPages/HomeDashboard/HomeDashboard.cshtml", projects);
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
