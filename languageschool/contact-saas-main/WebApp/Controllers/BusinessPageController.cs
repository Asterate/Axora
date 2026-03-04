using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using App.DAL.EF;
using App.Domain.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Helpers;
using App.BLL;
using Microsoft.AspNetCore.Identity;
using App.Domain.Identity;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;

namespace WebApp.Controllers;

[Authorize]
public class BusinessPageController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public BusinessPageController(AppDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }


    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Business Page";
        
        // Get all companies owned by the current user
        var companies = await CompanyOwnerService.GetCompaniesOwnedByUser(_context, User.UserId());
        return View(companies);
    }

    // GET: BusinessPage/CreateCompany
    public IActionResult CreateCompany()
    {
        ViewData["Title"] = "Create Company";
        return View();
    }

    // POST: BusinessPage/CreateCompany
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCompany([Bind("CompanyName,CompanyAddress,CompanyPhoneNumber,CompanyEmail,Id")] Company company)
    {
        if (ModelState.IsValid)
        {
            company.Id = Guid.NewGuid();
            _context.Add(company);
            
            // Associate the current user with the company
            var companyUser = new CompanyUser
            {
                CompanyId = company.Id,
                AppUserId = User.UserId(),
                Roles = ECompanyRoles.None // Start with no roles, will get owner role when approved
            };
            _context.CompanyUsers.Add(companyUser);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(company);
    }

    // GET: BusinessPage/CompanyDesktop/5
    public async Task<IActionResult> CompanyDesktop(Guid id)
    {
        ViewData["Title"] = "Company Desktop";
        
        // Get the company by id
        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == id);
            
        if (company == null)
        {
            return NotFound();
        }
        
        return View(company);
    }

    // GET: BusinessPage/CompanyAccount/5
    public async Task<IActionResult> CompanyAccount(Guid id)
    {
        ViewData["Title"] = "Company Account";
        
        // Get the company by id
        var company = await _context.Companies
            .Include(c => c.CompanyUsers)
            .ThenInclude(cu => cu.AppUser)
            .FirstOrDefaultAsync(c => c.Id == id);
            
        if (company == null)
        {
            return NotFound();
        }
        
        // Load courses separately
        var courses = await _context.Courses
            .Where(c => c.CompanyId == id)
            .Include(c => c.Language)
            .Include(c => c.Level)
            .Include(c => c.Teacher)
            .ToListAsync();
            
        ViewBag.Courses = courses;
        ViewBag.CompanyId = id;
        ViewBag.CompanyName = company.CompanyName;
        return View(company);
    }

    // GET: BusinessPage/CreateCourse/5
    public async Task<IActionResult> CreateCourse(Guid id)
    {
        ViewData["Title"] = "Create Course";
        
        // Get the company
        var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
        if (company == null)
        {
            return NotFound();
        }
        
        // Load required data for dropdowns
        ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageName");
        ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelName");
        ViewData["TeacherId"] = new SelectList(_context.Teachers.Where(t => t.CompanyId == id), "Id", "TeacherFullName");
        ViewBag.CompanyId = id;
        ViewBag.CompanyName = company.CompanyName;
        
        return View();
    }

    // POST: BusinessPage/CreateCourse/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCourse(Guid id, [Bind("CourseName,CourseDescription,CourseSeason,LanguageId,LevelId,TeacherId")] Course course)
    {
        if (ModelState.IsValid)
        {
            course.Id = Guid.NewGuid();
            course.CompanyId = id;
            course.CourseCreatedAt = DateTime.Now;
            course.CourseUpdatedAt = DateTime.Now;
            _context.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CompanyAccount), new { id });
        }
        
        // Reload data for dropdowns
        ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageName", course.LanguageId);
        ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "LevelName", course.LevelId);
        ViewData["TeacherId"] = new SelectList(_context.Teachers.Where(t => t.CompanyId == id), "Id", "TeacherFullName", course.TeacherId);
        
        var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
        ViewBag.CompanyId = id;
        ViewBag.CompanyName = company?.CompanyName;
        
        return View(course);
    }

    // GET: BusinessPage/CourseDesktop/5
    public async Task<IActionResult> CourseDesktop(Guid id)
    {
        ViewData["Title"] = "Course Desktop";
        
        // Get the course with related data
        var course = await _context.Courses
            .Include(c => c.Company)
            .Include(c => c.Language)
            .Include(c => c.Level)
            .Include(c => c.Teacher)
            .FirstOrDefaultAsync(c => c.Id == id);
            
        if (course == null)
        {
            return NotFound();
        }
        
        // Load materials separately
        var materials = await _context.Materials
            .Where(m => m.CourseId == id)
            .ToListAsync();
            
        // For now, treat all materials as both regular materials and homework
        // (since we don't have an IsHomework property)
        ViewBag.Materials = materials;
        ViewBag.Homework = materials;
        
        // For schedules, we need to find another way to associate with courses
        // For now, we'll just show an empty schedule
        ViewBag.Schedules = new List<Schedule>();
        
        return View(course);
    }

    // POST: BusinessPage/CompanyAccount/CreateUser
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(Guid companyId, string email, string firstName, string lastName, int role)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var companyRole = (ECompanyRoles)role;
                var (newUser, initialPassword) = await CompanyOwnerService.CreateCompanyUser(_context, email, firstName, lastName, companyRole, companyId, _userManager);
                
                // Store the initial password in TempData to display in the view
                TempData["InitialPassword"] = initialPassword;
                TempData["NewUserId"] = newUser.Id.ToString();
                
                return RedirectToAction(nameof(CompanyAccount), new { id = companyId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating user: {ex.Message}");
            }
        }
        
        var company = await _context.Companies
            .Include(c => c.CompanyUsers)
            .ThenInclude(cu => cu.AppUser)
            .FirstOrDefaultAsync(c => c.Id == companyId);
            
        ViewBag.CompanyId = companyId;
        ViewBag.CompanyName = company?.CompanyName;
        return View(nameof(CompanyAccount), company);
    }

    // GET: BusinessPage/PasswordLink/5
    public async Task<IActionResult> PasswordLink(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return NotFound();
        }
        
        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var callbackUrl = Url.Page(
            "/Account/ResetPassword",
            pageHandler: null,
            values: new { area = "Identity", code },
            protocol: Request.Scheme);

        var url = HtmlEncoder.Default.Encode(callbackUrl!);

        ViewBag.AppUser = user;
        ViewBag.PasswordLink = url;
        return View();
    }
}