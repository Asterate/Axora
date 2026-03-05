using App.DAL.EF;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.BLL;

public class CourseService
{
    //TODO: what course can do? (ex: enrollement)
    public static async Task<List<Course>> LoadCourses(AppDbContext context, Guid companyId)
    {
        return await context.Courses
            .Include(c => c.Language)
            .Include(c => c.Level)
            .Include(c => c.Teacher)
            .Where(c => c.CompanyId == companyId)
            .ToListAsync();
    }

    public static async Task CreateCourse(AppDbContext context,
        Guid companyId,
        string courseName,
        string courseDescription,
        string courseSeason,
        Guid languageId,
        Guid levelId,
        Guid teacherId)
    {
        var course = new Course()
        {
            CompanyId = companyId,
            CourseName = courseName,
            CourseDescription = courseDescription,
            CourseSeason = courseSeason,
            LanguageId = languageId,
            TeacherId = teacherId,
            LevelId = levelId
        };
        await context.Courses.AddAsync(course);
        await context.SaveChangesAsync();
    }

    
    
}