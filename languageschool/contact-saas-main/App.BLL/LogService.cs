using App.DAL.EF;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace App.BLL;

public class LogService
{
    // Generic method to create entity log items for any entity type
    public static async Task<List<EntityLogItemViewModel>> CreateEntityLogItems<T>(
        AppDbContext context, 
        Guid courseId, 
        Func<T, bool> filter, 
        Func<T, EntityLogItemViewModel> mapper,
        string entityName,
        string entityIcon,
        string logPageAction,
        string logPageController,
        string noItemsMessage = null)
        where T : class
    {
        var items = await context.Set<T>()
            .Where(filter)
            .ToListAsync();

        var logItems = new List<EntityLogItemViewModel>();

        if (!items.Any())
        {
            return logItems;
        }

        foreach (var item in items)
        {
            var logItem = mapper(item);
            logItems.Add(logItem);
        }

        return logItems;
    }

    // Specific method for attendance records
    public static async Task<List<EntityLogItemViewModel>> CreateAttendanceLogItems(
        AppDbContext context, 
        Guid courseId,
        Guid enrollmentId)
    {
        var attendanceRecords = await context.AttendanceRecords
            .Where(ar => ar.EnrollmentId == enrollmentId)
            .Include(ar => ar.Enrollment)
            .ThenInclude(e => e.Student)
            .ToListAsync();

        var logItems = new List<EntityLogItemViewModel>();

        foreach (var record in attendanceRecords)
        {
            var student = record.Enrollment.Student;
            var logItem = new EntityLogItemViewModel
            {
                Id = student.Id,
                Title = $"{student.StudentFirstName} {student.StudentLastName}",
                Subtitle = student.StudentEmail,
                Icon = "fas fa-user-graduate",
                BadgeText = $"{record.Attendance ? "Present" : "Absent"}",
                BadgeClass = record.Attendance ? "badge-success" : "badge-danger",
                LogActionText = "View Details",
                LogActionIcon = "fas fa-info-circle",
                RouteValues = new Dictionary<string, string>
                {
                    { "enrollmentId", enrollmentId.ToString() },
                    { "courseId", courseId.ToString() }
                },
                AdditionalData = new Dictionary<string, string>
                {
                    { "Date", record.AttendanceRecordCreatedAt.ToString("MMM dd, yyyy") },
                    { "Status", record.Attendance ? "Present" : "Absent" }
                }
            };

            logItems.Add(logItem);
        }

        return logItems;
    }

    // Specific method for placement test records
    public static async Task<List<EntityLogItemViewModel>> CreatePlacementTestLogItems(
        AppDbContext context, 
        Guid courseId,
        Guid studentId)
    {
        var placementTests = await context.StudentPlacementTests
            .Where(spt => spt.StudentId == studentId)
            .Include(spt => spt.PlacementTest)
            .ThenInclude(pt => pt.Level)
            .ToListAsync();

        var logItems = new List<EntityLogItemViewModel>();

        foreach (var test in placementTests)
        {
            var logItem = new EntityLogItemViewModel
            {
                Id = test.Id,
                Title = $"Placement Test - {test.PlacementTest?.PlacementTestName ?? "Unknown"}",
                Subtitle = test.PlacementTest?.PlacementTestDescription,
                Icon = "fas fa-graduation-cap",
                BadgeText = test.PlacementTest?.PlacementTestGrade,
                BadgeClass = "badge-info",
                LogActionText = "View Results",
                LogActionIcon = "fas fa-chart-bar",
                RouteValues = new Dictionary<string, string>
                {
                    { "studentId", studentId.ToString() },
                    { "courseId", courseId.ToString() }
                },
                AdditionalData = new Dictionary<string, string>
                {
                    { "Level", test.PlacementTest?.Level?.LevelName ?? "N/A" },
                    { "Grade", test.PlacementTest?.PlacementTestGrade ?? "N/A" },
                    { "Passed", test.PlacementTest?.PlacementTestPassed == true ? "Yes" : "No" }
                }
            };

            logItems.Add(logItem);
        }

        return logItems;
    }

    // Helper method to create a complete EntityLogViewModel
    public static EntityLogViewModel CreateEntityLogViewModel(
        string entityName,
        string entityIcon,
        string logPageAction,
        string logPageController,
        List<EntityLogItemViewModel> items,
        string noItemsMessage = null)
    {
        return new EntityLogViewModel
        {
            EntityName = entityName,
            EntityIcon = entityIcon,
            LogPageAction = logPageAction,
            LogPageController = logPageController,
            Items = items,
            NoItemsMessage = noItemsMessage
        };
    }
}