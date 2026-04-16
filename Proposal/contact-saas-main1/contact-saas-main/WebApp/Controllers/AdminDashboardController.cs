using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
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

        return View(model);
    }
}
