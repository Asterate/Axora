using System.Security.Claims;
using App.BLL;
using App.DAL.EF;
using App.Domain.Entities;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class TeacherPageController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public TeacherPageController(AppDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> TeacherDesktop(Guid id, Guid? selectedTeacherId = null)
    {
        var course = await _context.Courses
            .Include(c => c.Teacher)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null) return NotFound();

        // Get all teachers for the company
        var allTeachers = await _context.Teachers
            .Where(t => t.CompanyId == course.CompanyId)
            .ToListAsync();

        if (!allTeachers.Any()) return RedirectToAction("CourseDesktop", "CoursePage");

        // Determine selected teacher
        Guid teacherIdToUse;
        if (selectedTeacherId.HasValue && allTeachers.Any(t => t.Id == selectedTeacherId.Value))
        {
            teacherIdToUse = selectedTeacherId.Value;
        }
        else if (course.TeacherId != Guid.Empty && allTeachers.Any(t => t.Id == course.TeacherId))
        {
            teacherIdToUse = course.TeacherId;
        }
        else
        {
            teacherIdToUse = allTeachers.First().Id;
        }

        var selectedTeacher = allTeachers.First(t => t.Id == teacherIdToUse);

        // Get availabilities and teacher certificates for the selected teacher
        var availabilities = await _context.Availabilities
            .Where(a => a.TeacherId == teacherIdToUse)
            .ToListAsync();

        var teacherCertificates = await _context.TeacherCertificates
            .Include(c => c.Language)
            .Include(c => c.Level)
            .Where(c => c.TeacherId == teacherIdToUse)
            .ToListAsync();

        var vm = new TeacherDesktopViewModel
        {
            AllTeachers = allTeachers,
            SelectedTeacherId = teacherIdToUse,
            SelectedTeacher = selectedTeacher,
            CourseId = id,
            Availabilities = availabilities,
            TeacherCertificates = teacherCertificates
        };

        if (!availabilities.Any())
        {
            vm.NoAvailabilitiesMessage = "No availability slots defined yet. Add your first availability to get started!";
        }

        if (!teacherCertificates.Any())
        {
            vm.NoCertificatesMessage = "No teacher certificates added yet. Add your first certificate to get started!";
        }

        return View(vm);
    }

    // Availability CRUD

    public async Task<IActionResult> CreateAvailability(Guid teacherId)
    {
        var teacher = await _context.Teachers.FindAsync(teacherId);
        if (teacher == null) return NotFound();

        // Get all teachers for the same company
        var teachers = await _context.Teachers
            .Where(t => t.CompanyId == teacher.CompanyId)
            .Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = $"{t.TeacherFirstName} {t.TeacherLastName}"
            })
            .ToListAsync();

        var vm = new CreateAvailabilityViewModel
        {
            TeacherId = teacherId,
            TeacherName = $"{teacher.TeacherFirstName ?? ""} {teacher.TeacherLastName ?? ""}",
            Teachers = teachers
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAvailability(Guid teacherId, CreateAvailabilityViewModel vm)
    {
        if (ModelState.IsValid)
        {
            Availability? availability;

            if (vm.Id == Guid.Empty)
            {
                // Create new
                availability = new Availability
                {
                    Id = Guid.NewGuid(),
                    TeacherId = vm.TeacherId,
                    AvailabilityStartDate = vm.AvailabilityStartDate,
                    AvailabilityEndDate = vm.AvailabilityEndDate
                };
                _context.Availabilities.Add(availability);
            }
            else
            {
                // Update existing
                availability = await _context.Availabilities.FindAsync(vm.Id);
                if (availability == null) return NotFound();

                availability.AvailabilityStartDate = vm.AvailabilityStartDate;
                availability.AvailabilityEndDate = vm.AvailabilityEndDate;
                _context.Availabilities.Update(availability);
            }

            await _context.SaveChangesAsync();

            // Find the course associated with this teacher to redirect back to TeacherDesktop
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.TeacherId == vm.TeacherId);
            if (course != null)
            {
                return RedirectToAction("TeacherDesktop", new { id = course.Id, selectedTeacherId = vm.TeacherId });
            }

            return NotFound();
        }

        var teacher = await _context.Teachers.FindAsync(vm.TeacherId);
        vm.TeacherName = $"{teacher?.TeacherFirstName} {teacher?.TeacherLastName}";
        
        // Reload teachers list
        var teachers = await _context.Teachers
            .Where(t => teacher != null && t.CompanyId == teacher.CompanyId)
            .Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = $"{t.TeacherFirstName} {t.TeacherLastName}"
            })
            .ToListAsync();
        vm.Teachers = teachers;
        
        return View(vm);
    }

    public async Task<IActionResult> EditAvailability(Guid id)
    {
        var availability = await _context.Availabilities.FindAsync(id);
        if (availability == null) return NotFound();

        var teacher = await _context.Teachers.FindAsync(availability.TeacherId);
        if (teacher == null) return NotFound();

        var vm = new CreateAvailabilityViewModel
        {
            Id = availability.Id,
            TeacherId = availability.TeacherId,
            TeacherName = $"{teacher.TeacherFirstName ?? ""} {teacher.TeacherLastName ?? ""}",
            AvailabilityStartDate = availability.AvailabilityStartDate,
            AvailabilityEndDate = availability.AvailabilityEndDate
        };

        return View("CreateAvailability", vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAvailability(Guid id, Guid teacherId)
    {
        var availability = await _context.Availabilities.FindAsync(id);
        if (availability != null)
        {
            _context.Availabilities.Remove(availability);
            await _context.SaveChangesAsync();
        }

        var course = await _context.Courses.FirstOrDefaultAsync(c => c.TeacherId == teacherId);
        if (course != null)
        {
            return RedirectToAction("TeacherDesktop", new { id = course.Id });
        }

        return NotFound();
    }

    // Teacher Certificate CRUD

    public async Task<IActionResult> CreateTeacherCertificate(Guid teacherId)
    {
        var teacher = await _context.Teachers.FindAsync(teacherId);
        if (teacher == null) return NotFound();

        // Get all available languages and levels
        var languages = await _context.Languages
            .Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.LanguageName
            })
            .ToListAsync();

        var levels = await _context.Levels
            .Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.LevelName
            })
            .ToListAsync();

        // Get all teachers for the same company
        var teachers = await _context.Teachers
            .Where(t => t.CompanyId == teacher.CompanyId)
            .Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = $"{t.TeacherFirstName} {t.TeacherLastName}"
            })
            .ToListAsync();

        var vm = new CreateTeacherCertificateViewModel
        {
            TeacherId = teacherId,
            TeacherName = $"{teacher.TeacherFirstName ?? ""} {teacher.TeacherLastName ?? ""}",
            Teachers = teachers,
            Languages = languages,
            Levels = levels
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTeacherCertificate(Guid teacherId, CreateTeacherCertificateViewModel vm)
    {
        if (ModelState.IsValid)
        {
            TeacherCertificate? teacherCertificate;

            if (vm.Id == Guid.Empty)
            {
                // Create new
                teacherCertificate = new TeacherCertificate
                {
                    Id = Guid.NewGuid(),
                    TeacherId = vm.TeacherId,
                    LanguageId = vm.LanguageId,
                    LevelId = vm.LevelId,
                    TeacherCertificateName = vm.TeacherCertificateName,
                    TeacherCertificateDescription = vm.TeacherCertificateDescription,
                    TeacherCertificateGivenOut = vm.TeacherCertificateGivenOut
                };
                _context.TeacherCertificates.Add(teacherCertificate);
            }
            else
            {
                // Update existing
                teacherCertificate = await _context.TeacherCertificates.FindAsync(vm.Id);
                if (teacherCertificate == null) return NotFound();

                teacherCertificate.LanguageId = vm.LanguageId;
                teacherCertificate.LevelId = vm.LevelId;
                teacherCertificate.TeacherCertificateName = vm.TeacherCertificateName;
                teacherCertificate.TeacherCertificateDescription = vm.TeacherCertificateDescription;
                teacherCertificate.TeacherCertificateGivenOut = vm.TeacherCertificateGivenOut;
                _context.TeacherCertificates.Update(teacherCertificate);
            }

            await _context.SaveChangesAsync();

            var course = await _context.Courses.FirstOrDefaultAsync(c => c.TeacherId == vm.TeacherId);
            if (course != null)
            {
                return RedirectToAction("TeacherDesktop", new { id = course.Id, selectedTeacherId = vm.TeacherId });
            }

            return NotFound();
        }

        var teacher = await _context.Teachers.FindAsync(vm.TeacherId);
        if (teacher != null)
        {
            vm.TeacherName = $"{teacher.TeacherFirstName ?? ""} {teacher.TeacherLastName ?? ""}";
        }
        
        // Reload teachers list
        var teachers = await _context.Teachers
            .Where(t => teacher != null && t.CompanyId == teacher.CompanyId)
            .Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = $"{t.TeacherFirstName} {t.TeacherLastName}"
            })
            .ToListAsync();
        vm.Teachers = teachers;
        
        vm.Languages = await _context.Languages
            .Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.LanguageName
            })
            .ToListAsync();
        vm.Levels = await _context.Levels
            .Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.LevelName
            })
            .ToListAsync();

        return View(vm);
    }

    public async Task<IActionResult> EditTeacherCertificate(Guid id)
    {
        var teacherCertificate = await _context.TeacherCertificates.FindAsync(id);
        if (teacherCertificate == null) return NotFound();

        var teacher = await _context.Teachers.FindAsync(teacherCertificate.TeacherId);
        if (teacher == null) return NotFound();

        var languages = await _context.Languages
            .Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.LanguageName
            })
            .ToListAsync();

        var levels = await _context.Levels
            .Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.LevelName
            })
            .ToListAsync();

        var vm = new CreateTeacherCertificateViewModel
        {
            Id = teacherCertificate.Id,
            TeacherId = teacherCertificate.TeacherId,
            TeacherName = $"{teacher.TeacherFirstName ?? ""} {teacher.TeacherLastName ?? ""}",
            LanguageId = teacherCertificate.LanguageId,
            LevelId = teacherCertificate.LevelId,
            TeacherCertificateName = teacherCertificate.TeacherCertificateName,
            TeacherCertificateDescription = teacherCertificate.TeacherCertificateDescription,
            TeacherCertificateGivenOut = teacherCertificate.TeacherCertificateGivenOut,
            Languages = languages,
            Levels = levels
        };

        return View("CreateTeacherCertificate", vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteTeacherCertificate(Guid id, Guid teacherId)
    {
        var teacherCertificate = await _context.TeacherCertificates.FindAsync(id);
        if (teacherCertificate != null)
        {
            _context.TeacherCertificates.Remove(teacherCertificate);
            await _context.SaveChangesAsync();
        }

        var course = await _context.Courses.FirstOrDefaultAsync(c => c.TeacherId == teacherId);
        if (course != null)
        {
            return RedirectToAction("TeacherDesktop", new { id = course.Id });
        }

        return NotFound();
    }
}
