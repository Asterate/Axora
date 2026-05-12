using App.Domain.Entities;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Lab.Infrastructure.Repositories;

internal sealed class ScheduleRepository : IScheduleRepository
{
    private readonly ResearchDbContext _context;

    public ScheduleRepository(ResearchDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Schedule>> GetAllAsync()
        => await _context.Schedules.ToListAsync();

    public async Task<Schedule?> GetByIdAsync(Guid id)
        => await _context.Schedules.FindAsync(id);

    public async Task AddAsync(Schedule entity)
        => await _context.Schedules.AddAsync(entity);

    public void Update(Schedule entity)
        => _context.Schedules.Update(entity);

    public void Delete(Schedule entity)
        => _context.Schedules.Remove(entity);
}