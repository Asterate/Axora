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
using App.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using WebApp.Areas.Root.ViewModels;
using WebApp.ViewModels;
using static App.BLL.CourseService;

namespace WebApp.Controllers;

[Authorize]
public class BusinessPageController(AppDbContext context, UserManager<AppUser> userManager) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Business Page";

        // Get all companies owned by the current user
        var companies = await CompanyOwnerService.GetCompaniesOwnedByUser(context, User.UserId());
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
    public async Task<IActionResult> CreateCompany(
        [Bind("CompanyName,CompanyAddress,CompanyPhoneNumber,CompanyEmail,Id")] Company company)
    {
        if (ModelState.IsValid)
        {
            company.Id = Guid.NewGuid();
            context.Add(company);

            // Associate the current user with the company
            var userId = userManager.GetUserId(User);
            var appUser = await userManager.FindByIdAsync(userId ?? throw new InvalidOperationException());
            if (appUser == null) throw new InvalidOperationException();
            var appUserId = appUser.Id;
            var companyUser = new CompanyUser
            {
                CompanyId = company.Id,
                AppUserId = appUserId,
                Roles = ECompanyRoles.None // Start with no roles, will get owner role when approved
            };
            context.CompanyUsers.Add(companyUser);
            await UserRoleHelper.SyncCompanyUserRolesToIdentityAsync(userManager, appUser, companyUser.Roles);

            var companyConfig = new CompanyConfig
            {
                CompanyId = company.Id,
                Company = company
            };
            context.CompanyConfigs.Add(companyConfig);
            
            var companySubs = new Subs()
            {
                CompanyId = company.Id,
                Company = company
            };
            context.Subs.Add(companySubs);

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(company);
    }

    // GET: BusinessPage/CompanyDesktop/5
    public async Task<IActionResult> CompanyDesktop(Guid id)
    {
        var company = await context.Companies
            .FirstOrDefaultAsync(c => c.Id == id);

        if (company == null)
        {
            return NotFound();
        }
        var companyLanguages = await context.Languages
            .Where(l => l.CompanyId == id)
            .ToListAsync();
        var courses = await LoadCourses(context, id);
        var levels = await context.Levels.ToListAsync();
        var lm = new CompanyDesktopViewModel
        {
            Company = company,
            Courses = courses,
            Languages = companyLanguages,
            Levels = levels
        };

        return View(lm);
    }

    // GET: BusinessPage/CompanyAccount/5
    public async Task<IActionResult> CompanyAccount(Guid id)
    {
        ViewData["Title"] = "Company Account";

        // Get the company by id
        var company = await context.Companies
            .Include(c => c.CompanyUsers)
            .ThenInclude(cu => cu.AppUser)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (company == null)
        {
            return NotFound();
        }

        // Load courses separately
        var courses = await context.Courses
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
    
    // POST: BusinessPage/CompanyAccount/CreateUser
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(Guid companyId, string email, string firstName, string lastName,
        int role, string? teacherPhone = null, string? teacherAddress = null, string? teacherNativeLanguage = null, 
        string? teacherNationality = null, string? teacherGender = null)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var companyRole = (ECompanyRoles)role;
                
                // Validate teacher fields if role is teacher
                if (companyRole == ECompanyRoles.Teacher)
                {
                    if (string.IsNullOrEmpty(teacherPhone) || string.IsNullOrEmpty(teacherAddress) || 
                        string.IsNullOrEmpty(teacherNativeLanguage) || string.IsNullOrEmpty(teacherNationality) || 
                        string.IsNullOrEmpty(teacherGender))
                    {
                        ModelState.AddModelError("", "All teacher information fields are required when creating a teacher");
                    }
                }

                if (ModelState.IsValid)
                {
                    var (newUser, initialPassword) = await CompanyOwnerService.CreateCompanyUser(context, email, firstName,
                        lastName, companyRole, companyId, userManager, teacherPhone, teacherAddress, 
                        teacherNativeLanguage, teacherNationality, teacherGender);

                    // Store the initial password in TempData to display in the view
                    TempData["InitialPassword"] = initialPassword;
                    TempData["NewUserId"] = newUser.Id.ToString();

                    return RedirectToAction(nameof(CompanyAccount), new { id = companyId });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating user: {ex.Message}");
            }
        }

        var company = await context.Companies
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
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return NotFound();
        }

        var code = await userManager.GeneratePasswordResetTokenAsync(user);

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

    public async Task<IActionResult> CompanySettings(Guid id)
    {

        var companyConfig = await context.CompanyConfigs
            .Include(c => c.Company)
            .FirstOrDefaultAsync(m => m.CompanyId == id);
        if (companyConfig == null)
            return NotFound();
        
            var companyLanguages = await context.Languages
                .Where(l => l.CompanyId == companyConfig.CompanyId)
                .Include(l => l.Level)
                .ToListAsync();

            // Get all levels that are associated with the company's languages
            var levels = await context.Languages
                .Where(l => l.CompanyId == companyConfig.CompanyId)
                .Include(l => l.Level)
                .Select(l => l.Level)
                .Where(l => l != null)
                .Distinct()
                .ToListAsync();
        
            var companySettings = new CompanySettingsViewModel
        {
            CompanyConfig = companyConfig,
            Languages = companyLanguages,
            Company = companyConfig.Company!,
            Levels = levels
                .Select(l => new SelectListItem
                {
                    Value = l!.Id.ToString(),
                    Text = l.LevelName
                })
                .ToList()
        };
        return View(companySettings);
    }
    
        // GET: CompanyConfig/Edit/5
        public async Task<IActionResult> EditSettings(Guid id)
        {
            var companyConfig = await context.CompanyConfigs
                .Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            
            if (companyConfig == null)
            {
                return NotFound();
            }
            
            var companyLanguages = await context.Languages
                .Where(l => l.CompanyId == companyConfig.CompanyId)
                .ToListAsync();

            // Get all levels that are associated with the company's languages
            var levels = await context.Languages
                .Where(l => l.CompanyId == companyConfig.CompanyId)
                .Include(l => l.Level)
                .Select(l => l.Level!)
                .GroupBy(l => l.Id)
                .Select(g => g.First())
                .ToListAsync();

           if (companyConfig.Company != null)
           {
               var viewModel = new CompanySettingsViewModel
               {
                   CompanyConfig = companyConfig,
                   Company = companyConfig.Company,
                   Languages = companyLanguages,
                   Levels = levels
                       .Select(l => new SelectListItem
                       {
                           Value = l!.Id.ToString(),
                           Text = l.LevelName
                       })
                       .ToList(),
                   EditLanguage = null
               };
            
               return View(viewModel);
           }
           return View(new CompanySettingsViewModel());
        }

        // POST: CompanyConfig/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSettings(Guid id, CompanySettingsViewModel viewModel)
        {
            ModelState.Remove("Company");
            ModelState.Remove("Languages");
            ModelState.Remove("Levels");

            if (!ModelState.IsValid)
            {
                viewModel.Languages = await context.Languages
                    .Where(l => l.CompanyId == id)
                    .ToListAsync();
                viewModel.Levels = (await context.Levels.ToListAsync())
                    .Select(l => new SelectListItem { Value = l.Id.ToString(), Text = l.LevelName })
                    .ToList();
                viewModel.Company = (await context.Companies.FindAsync(id))!;
                return View(viewModel);
            }

            var existingConfig = await context.CompanyConfigs
                .FirstOrDefaultAsync(c => c.CompanyId == id);

            if (existingConfig == null)
                return NotFound();
            
            existingConfig.CompanyConfigTimeZone = viewModel.CompanyConfig.CompanyConfigTimeZone;
            existingConfig.CompanyConfigThemeColour = viewModel.CompanyConfig.CompanyConfigThemeColour;
            existingConfig.CompanyConfigCompanyLogoPath = viewModel.CompanyConfig.CompanyConfigCompanyLogoPath;
            existingConfig.CompanyConfigMaxStudentsPerCourse = viewModel.CompanyConfig.CompanyConfigMaxStudentsPerCourse;
            existingConfig.CompanyConfigAllowOneOnOneSessions = viewModel.CompanyConfig.CompanyConfigAllowOneOnOneSessions;
            existingConfig.CompanyConfigEnableMaterialTracking = viewModel.CompanyConfig.CompanyConfigEnableMaterialTracking;
            existingConfig.CompanyConfigEnableCertificates = viewModel.CompanyConfig.CompanyConfigEnableCertificates;
            existingConfig.CompanyConfigVisibility = viewModel.CompanyConfig.CompanyConfigVisibility;
            existingConfig.CompanyConfigUpdatedAt = DateTime.Now;
            context.CompanyConfigs.Update(existingConfig);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(CompanySettings), new { id });
        }
    

        // POST: BusinessPage/AddLanguage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLanguage(Guid companyId, string languageName, string languageDescription, Guid newLanguageLevelId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(CompanySettings), new { id = companyId });
            }

            var language = new Language
            {
                CompanyId = companyId,
                LanguageName = languageName,
                LanguageDescription = languageDescription,
                LevelId = newLanguageLevelId
            };

            try
            {
                context.Languages.Add(language);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error adding language");
            }

            return RedirectToAction(nameof(CompanySettings), new { id = companyId });
        }

        // POST: BusinessPage/DeleteLanguage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLanguage(Guid languageId, Guid companyId)
        {
            var language = await context.Languages.FindAsync(languageId);
            if (language != null)
            {
                context.Languages.Remove(language);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(EditSettings), new { id = companyId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLanguage(Guid languageId, Guid companyId, string languageName, string languageDescription, Guid levelId)
        {
            var language = await context.Languages.FindAsync(languageId);
            if (language == null || language.CompanyId != companyId)
                return NotFound();

            language.LanguageName = languageName;
            language.LanguageDescription = languageDescription;
            language.LevelId = levelId;
            context.Languages.Update(language);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(EditSettings), new { id = companyId });
            
        }

        public RedirectToActionResult CreateCourse(Guid companyId)
        {
            return RedirectToAction("CreateCourse", "CoursePage", new { companyId });
        }
}