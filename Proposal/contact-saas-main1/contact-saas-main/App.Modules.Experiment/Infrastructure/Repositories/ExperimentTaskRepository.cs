using App.Domain.Entities;
using App.Modules.Experiment.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Experiment.Infrastructure.Repositories;

internal sealed class ExperimentTaskRepository : IExperimentTaskRepository
{
    private readonly ExperimentDbContext _context;

    public ExperimentTaskRepository(ExperimentDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExperimentTask>> GetAllAsync()
        => await _context.ExperimentTasks.ToListAsync();

    public async Task<ExperimentTask?> GetByIdAsync(Guid id)
        => await _context.ExperimentTasks.FindAsync(id);

    public async Task AddAsync(ExperimentTask entity)
        => await _context.ExperimentTasks.AddAsync(entity);

    public void Update(ExperimentTask entity)
        => _context.ExperimentTasks.Update(entity);

    public void Delete(ExperimentTask entity)
        => _context.ExperimentTasks.Remove(entity);
}