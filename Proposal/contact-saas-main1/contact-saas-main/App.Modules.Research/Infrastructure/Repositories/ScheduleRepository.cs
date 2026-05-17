using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Project.Infrastructure.Repositories;

internal sealed class ScheduleRepository : IScheduleRepository
{
    private readonly ResearchDbContext _context;

    public ScheduleRepository(ResearchDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Schedule>> GetAllAsync()
        => await _context.Schedules
            .Include(p => p.Experiment)
            .ToListAsync();

    public async Task<Schedule?> GetByIdAsync(Guid id)
        => await _context.Schedules
            .Include(p => p.Experiment)
            .FirstOrDefaultAsync(s => s.Id == id);

    public async Task AddAsync(Schedule entity)
        => await _context.Schedules.AddAsync(entity);

    public void Update(Schedule entity)
        => _context.Schedules.Update(entity);

    public void Delete(Schedule entity)
        => _context.Schedules.Remove(entity);
}