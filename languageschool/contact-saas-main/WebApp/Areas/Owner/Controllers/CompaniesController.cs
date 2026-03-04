using App.BLL;
using App.DAL.EF;
using App.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Helpers;
using Microsoft.AspNetCore.Identity;
using App.Domain.Identity;

namespace WebApp.Areas.Owner.Controllers;

[Area("Owner")]
[Authorize]
public class CompaniesController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public CompaniesController(AppDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Owner/Companies
    public async Task<IActionResult> Index()
    {
        var userId = User.UserId();
        var companies = await CompanyOwnerService.GetCompaniesOwnedByUser(_context, userId);
        return View(companies);
    }

    // GET: Owner/Companies/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        // Check if user is the owner of the company
        var userId = User.UserId();
        var isOwner = await UserService.HasRoleInCompany(_context, id, userId, ECompanyRoles.Owner);
        
        if (!isOwner)
        {
            return Forbid();
        }

        var company = await CompanyOwnerService.GetCompanyWithUsers(_context, id);
        if (company == null)
        {
            return NotFound();
        }

        return View(company);
    }

    // GET: Owner/Companies/CreateAdmin
    public async Task<IActionResult> CreateAdmin(Guid companyId)
    {
        // Check if user is the owner of the company
        var userId = User.UserId();
        var isOwner = await UserService.HasRoleInCompany(_context, companyId, userId, ECompanyRoles.Owner);
        
        if (!isOwner)
        {
            return Forbid();
        }

        ViewBag.CompanyId = companyId;
        return View();
    }

    // POST: Owner/Companies/CreateAdmin
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAdmin(Guid companyId, string email, string firstName, string lastName)
    {
        // Check if user is the owner of the company
        var userId = User.UserId();
        var isOwner = await UserService.HasRoleInCompany(_context, companyId, userId, ECompanyRoles.Owner);
        
        if (!isOwner)
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await CompanyOwnerService.CreateCompanyUser(_context, email, firstName, lastName, ECompanyRoles.Admin, companyId, _userManager);
                return RedirectToAction(nameof(Details), new { id = companyId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating admin: {ex.Message}");
            }
        }

        ViewBag.CompanyId = companyId;
        return View();
    }

    // GET: Owner/Companies/CreateManager
    public async Task<IActionResult> CreateManager(Guid companyId)
    {
        // Check if user is the owner of the company
        var userId = User.UserId();
        var isOwner = await UserService.HasRoleInCompany(_context, companyId, userId, ECompanyRoles.Owner);
        
        if (!isOwner)
        {
            return Forbid();
        }

        ViewBag.CompanyId = companyId;
        return View();
    }

    // POST: Owner/Companies/CreateManager
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateManager(Guid companyId, string email, string firstName, string lastName)
    {
        // Check if user is the owner of the company
        var userId = User.UserId();
        var isOwner = await UserService.HasRoleInCompany(_context, companyId, userId, ECompanyRoles.Owner);
        
        if (!isOwner)
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await CompanyOwnerService.CreateCompanyUser(_context, email, firstName, lastName, ECompanyRoles.Manager, companyId, _userManager);
                return RedirectToAction(nameof(Details), new { id = companyId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating manager: {ex.Message}");
            }
        }

        ViewBag.CompanyId = companyId;
        return View();
    }

    // GET: Owner/Companies/CreateTeacher
    public async Task<IActionResult> CreateTeacher(Guid companyId)
    {
        // Check if user is the owner of the company
        var userId = User.UserId();
        var isOwner = await UserService.HasRoleInCompany(_context, companyId, userId, ECompanyRoles.Owner);
        
        if (!isOwner)
        {
            return Forbid();
        }

        ViewBag.CompanyId = companyId;
        return View();
    }

    // POST: Owner/Companies/CreateTeacher
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTeacher(Guid companyId, string email, string firstName, string lastName)
    {
        // Check if user is the owner of the company
        var userId = User.UserId();
        var isOwner = await UserService.HasRoleInCompany(_context, companyId, userId, ECompanyRoles.Owner);
        
        if (!isOwner)
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await CompanyOwnerService.CreateCompanyUser(_context, email, firstName, lastName, ECompanyRoles.Teacher, companyId, _userManager);
                return RedirectToAction(nameof(Details), new { id = companyId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating teacher: {ex.Message}");
            }
        }

        ViewBag.CompanyId = companyId;
        return View();
    }
}
