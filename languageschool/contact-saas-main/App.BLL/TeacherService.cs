using App.DAL.EF;
using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.BLL;

public class TeacherService
{
    //TODO: what teacher can do?(crud sessions, make appointments)
    public static async Task<List<SelectListItem>> LoadCompanyTeachers(AppDbContext context, Guid id)
    {
        var teachers = await context.Teachers
            .Where(l => l.CompanyId == id)
            .ToListAsync();
        return teachers
            .Select(t => new SelectListItem
                { Value = t.Id.ToString(), Text = t.TeacherFirstName + " " + t.TeacherLastName })
            .ToList();

    }
    public static void AddAvailableTime()
    {
        
    }
    public static void DeleteAvailableTime()
    {
        
    }
    public static void UpdateAvailableTime()
    {
        
    }
    public static void AddSession()
    {
        
    }
    public static void DeleteSession()
    {
        
    }
    public static void UpdateSession()
    {
        
    }

    public static void ConfirmAttendance()
    {
        
    }
    public static void ConfirmCertificate()
    {
        
    }
    public static void AddTeacherCertificate()
    {
        
    }
    public static void DeleteTeacherCertificate()
    {
        
    }
    public static void UpdateTeacherCertificate()
    {
        
    }
}
