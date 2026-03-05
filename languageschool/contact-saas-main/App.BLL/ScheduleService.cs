using App.DAL.EF;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.BLL;

public class ScheduleService
{
    public static async Task<List<Schedule>> GetSchedulesByAvailabilityIds(AppDbContext context, List<Guid> availabilityIds)
    {
        return await context.Schedules
            .Where(s => availabilityIds.Contains(s.AvailabilityId))
            .ToListAsync();
    }

    public static async Task<List<Schedule>> GetSchedulesByTeacherId(AppDbContext context, Guid teacherId)
    {
        var teacherAvailabilities = await context.Availabilities
            .Where(a => a.TeacherId == teacherId)
            .Select(a => a.Id)
            .ToListAsync();
            
        return await GetSchedulesByAvailabilityIds(context, teacherAvailabilities);
    }

    public static async Task<Schedule?> GetScheduleById(AppDbContext context, Guid scheduleId)
    {
        return await context.Schedules
            .Include(s => s.Availability)
            .ThenInclude(a => a!.Teacher)
            .FirstOrDefaultAsync(s => s.Id == scheduleId);
    }

    public static async Task AddSchedule(AppDbContext context, Schedule schedule)
    {
        schedule.ScheduleCreatedAt = DateTime.Now;
        schedule.ScheduleModifiedAt = DateTime.Now;
        context.Schedules.Add(schedule);
        await context.SaveChangesAsync();
    }

    public static async Task UpdateSchedule(AppDbContext context, Schedule schedule)
    {
        schedule.ScheduleModifiedAt = DateTime.Now;
        context.Schedules.Update(schedule);
        await context.SaveChangesAsync();
    }

    public static async Task DeleteSchedule(AppDbContext context, Guid scheduleId)
    {
        var schedule = await context.Schedules.FindAsync(scheduleId);
        if (schedule != null)
        {
            context.Schedules.Remove(schedule);
            await context.SaveChangesAsync();
        }
    }

    public static async Task<List<Availability>> GetAvailabilitiesByTeacherId(AppDbContext context, Guid teacherId)
    {
        return await context.Availabilities
            .Where(a => a.TeacherId == teacherId)
            .ToListAsync();
    }
}