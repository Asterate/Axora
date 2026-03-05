using App.BLL;
using App.DAL.EF;
using App.Domain.Entities;
using App.Domain.Identity;
using App.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;
using WebApp.Views.BusinessPage;

namespace WebApp.Controllers;

public class CoursePageController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    public CoursePageController(AppDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    // Enrollment action
    [Authorize(Roles = "STUDENT,COMPANYMANAGER,COMPANYADMIN,COMPANYOWNER")]
    public async Task<IActionResult> Enroll(Guid id)
    {
        var course = await _context.Courses
            .Include(c => c.Language)
            .Include(c => c.Level)
            .Include(c => c.Teacher)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
        {
            return NotFound();
        }

        // Get subscription information for the company
        var subs = await _context.Subs
            .FirstOrDefaultAsync(s => s.CompanyId == course.CompanyId);

        var vm = new EnrollmentViewModel
        {
            CourseId = id,
            Course = course,
            Subs = subs,
            EnrollmentPlans = new List<SelectListItem>
            {
                new SelectListItem { Value = EEnrollmentPlan.Free.ToString(), Text = "Free" },
                new SelectListItem { Value = EEnrollmentPlan.Standard.ToString(), Text = "Standard" },
                new SelectListItem { Value = EEnrollmentPlan.Premium.ToString(), Text = "Premium" }
            }
        };

        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "STUDENT,COMPANYMANAGER,COMPANYADMIN,COMPANYOWNER,admin")]
    public async Task<IActionResult> Enroll(Guid id, EnrollmentViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            var course = await _context.Courses
                .Include(c => c.Language)
                .Include(c => c.Level)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(c => c.Id == id);
                
            vm.Course = course;
            vm.Subs = await _context.Subs.FirstOrDefaultAsync(s => s.CompanyId == course!.CompanyId);
            vm.EnrollmentPlans = new List<SelectListItem>
            {
                new SelectListItem { Value = EEnrollmentPlan.Free.ToString(), Text = "Free" },
                new SelectListItem { Value = EEnrollmentPlan.Standard.ToString(), Text = "Standard" },
                new SelectListItem { Value = EEnrollmentPlan.Premium.ToString(), Text = "Premium" }
            };
            return View(vm);
        }

        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null)
        {
            return Challenge();
        }

        var courseToEnroll = await _context.Courses
            .Include(c => c.Company)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (courseToEnroll == null)
        {
            return NotFound();
        }

        // Check if company user exists, create if not
        var companyUser = await _context.CompanyUsers
            .FirstOrDefaultAsync(cu => cu.AppUserId == currentUser.Id && cu.CompanyId == courseToEnroll.CompanyId);

        if (companyUser == null)
        {
            companyUser = new CompanyUser
            {
                AppUserId = currentUser.Id,
                CompanyId = courseToEnroll.CompanyId,
                Roles = ECompanyRoles.Student
            };
            _context.CompanyUsers.Add(companyUser);
            await _context.SaveChangesAsync();
        }

        // Create student
        var student = new Student
        {
            CompanyId = courseToEnroll.CompanyId,
            CompanyUserId = companyUser.Id,
            StudentFirstName = vm.StudentFirstName,
            StudentLastName = vm.StudentLastName,
            StudentEmail = vm.StudentEmail,
            StudentPhone = vm.StudentPhone,
            StudentAddress = vm.StudentAddress,
            StudentNativeLanguage = vm.StudentNativeLanguage,
            StudentNationality = vm.StudentNationality,
            StudentGender = vm.StudentGender,
            SubsId = vm.Subs?.Id
        };
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        await UserRoleHelper.SyncCompanyUserRolesToIdentityAsync(_userManager, currentUser, companyUser.Roles);

        // Create enrollment
        var enrollment = new Enrollment
        {
            CourseId = id,
            StudentId = student.Id,
            EnrollmentSeason = courseToEnroll?.CourseSeason ?? string.Empty,
            EnrollmentPlan = vm.EnrollmentPlan,
            EnrollmentCreatedAt = DateTime.Now,
            EnrollmentModifiedAt = DateTime.Now
        };
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        return RedirectToAction("CourseDesktop", new { id });
    }
    
    public async Task<IActionResult> CreateCourse(Guid id, Guid languageId)
    {
        var company = _context.Companies.FirstOrDefault(x => x.Id == id);
        if (company == null) NotFound();
        
        var vm = new CreateCourseViewModel
        {
            CompanyId = id,
            CompanyName = company?.CompanyName!,
            Languages = await LanguageService.CompanyLanguages(_context, id),
            Levels = new List<SelectListItem>(),
            Teachers = await TeacherService.LoadCompanyTeachers(_context, id),
            LanguageId = languageId,
            LanguageLevelMap = await LanguageService.GetLanguageLevelMap(_context, id)
        };
        if (vm.LanguageId != Guid.Empty && vm.LanguageLevelMap.ContainsKey(vm.LanguageId))
            vm.Levels = vm.LanguageLevelMap[vm.LanguageId];


        return View(vm);

    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "COMPANYMANAGER,COMPANYOWNER,admin")]
    public async Task<IActionResult> CreateCourse(Guid id, CreateCourseViewModel vm)
    {
        
        var newCourse = new Course
        {
            CompanyId = id,
            CourseName = vm.CourseName,
            CourseDescription = vm.CourseDescription,
            LanguageId = vm.LanguageId,
            LevelId = vm.LevelId,
            TeacherId = vm.TeacherId,
            CourseSeason = vm.CourseSeason
        };
        _context.Courses.Add(newCourse);
        await _context.SaveChangesAsync();

        return RedirectToAction("CompanyDesktop", "BusinessPage", new { id });

    }

    public IActionResult CompanyDesktop(Guid companyId)
    {
        return RedirectToAction("CompanyDesktop", "BusinessPage", new { companyId });
    }

    public async Task<IActionResult> CourseDesktop(Guid id)
        {
            var course = await _context.Courses
                .Include(c => c.Language)
                .Include(c => c.Level)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            // Check if current user is enrolled
            var isEnrolled = false;
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                var companyUser = await _context.CompanyUsers
                    .FirstOrDefaultAsync(cu => cu.AppUserId == currentUser.Id && cu.CompanyId == course.CompanyId);
                
                if (companyUser != null)
                {
                    var student = await _context.Students
                        .FirstOrDefaultAsync(s => s.CompanyUserId == companyUser.Id);
                    
                    if (student != null)
                    {
                        isEnrolled = await _context.Enrollments
                            .AnyAsync(e => e.CourseId == id && e.StudentId == student.Id);
                    }
                }
            }

            // Get materials for the course
            var materials = await _context.Materials
                .Where(m => m.CourseId == id)
                .ToListAsync();

            // Get schedules for the course (through teacher's availability)
            var schedules = new List<Schedule>();
            
                var teacherAvailabilities = await _context.Availabilities
                    .Where(a => a.TeacherId == course.TeacherId)
                    .Select(a => a.Id)
                    .ToListAsync();
                    
                schedules = await _context.Schedules
                    .Where(s => teacherAvailabilities.Contains(s.AvailabilityId))
                    .ToListAsync();

            // Get enrolled students and their attendance records
            var enrolledStudents = await _context.Enrollments
                .Where(e => e.CourseId == id)
                .Include(e => e.Student)
                .ToListAsync();
            
           
            
            // For demo purposes, if no materials or schedules, create some dummy data
            var vm = new CourseDesktopViewModel
            {
                Course = course,
                Materials = materials,
                Schedules = schedules,
                IsEnrolled = isEnrolled
            };
            
            if (!materials.Any())
            {
                vm.NoMaterialsMessage = "No materials available for this course yet. Add your first material to get started!";
            }
            
            if (!schedules.Any())
            {
                vm.NoSchedulesMessage = "No schedule available for this course yet. Contact your teacher for more information!";
            }

            return View(vm);
        }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "COMPANYMANAGER,COMPANYOWNER,admin")]
    public IActionResult CourseDelete(Guid id, Guid companyId)
    {
        var course = _context.Courses.FirstOrDefault(x => x.Id == id);
        if (course != null)
        {
        
            _context.Courses.Remove(course);
            _context.SaveChanges();
        
        }
        return RedirectToAction("CompanyDesktop", "BusinessPage", new { id = companyId });
    }

    public IActionResult CreateMaterial(Guid courseId)
    {
        return RedirectToAction("CreateMaterial", "MaterialPage", new { courseId });
    }

    [Authorize(Roles = "COMPANYMANAGER,COMPANYOWNER,admin")]
     public async Task<IActionResult> EditCourse(Guid id, Guid? languageId = null)
    {
        var course = await _context.Courses
            .Include(c => c.Company)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (course == null) return NotFound();

        var vm = new CreateCourseViewModel
        {
            CourseId = id,
            CompanyId = course.CompanyId,
            CompanyName = course.Company?.CompanyName ?? "",
            CourseName = course.CourseName,
            CourseDescription = course.CourseDescription,
            CourseSeason = course.CourseSeason,
            LanguageId = languageId ?? course.LanguageId,
            LevelId = course.LevelId,
            TeacherId = course.TeacherId,
            Languages = await LanguageService.CompanyLanguages(_context, course.CompanyId),
            LanguageLevelMap = await LanguageService.GetLanguageLevelMap(_context, course.CompanyId),
            Teachers = await TeacherService.LoadCompanyTeachers(_context, course.CompanyId)
        };

        // Fill Levels for the selected language
        if (vm.LanguageId != Guid.Empty && vm.LanguageLevelMap.ContainsKey(vm.LanguageId))
            vm.Levels = vm.LanguageLevelMap[vm.LanguageId];

        return View("CreateCourse", vm); // Reuse CreateCourse view
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "COMPANYMANAGER,COMPANYOWNER,admin")]
    public async Task<IActionResult> EditCourse(Guid id, CreateCourseViewModel vm)
    {
        ModelState.Remove("Languages");
        ModelState.Remove("Levels");
        ModelState.Remove("Teachers");
        ModelState.Remove("LanguageLevelMap");
        ModelState.Remove("CompanyName");

        var course = await _context.Courses.FindAsync(id);
        if (course == null) return NotFound();

        course.CourseName = vm.CourseName;
        course.CourseDescription = vm.CourseDescription;
        course.CourseSeason = vm.CourseSeason;
        course.LanguageId = vm.LanguageId;
        course.LevelId = vm.LevelId;
        course.TeacherId = vm.TeacherId;
        course.CourseUpdatedAt = DateTime.Now;
        
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();

        return RedirectToAction("CourseDesktop", "CoursePage", new { Id = id  });
    }
    
}