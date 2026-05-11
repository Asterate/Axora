using App.Domain.Entities;
using App.Modules.Experiment.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Experiment.Infrastructure.Repositories;

internal sealed class ExperimentTaskTypeRepository : IExperimentTaskTypeRepository
{
    private readonly ExperimentDbContext _context;

    public ExperimentTaskTypeRepository(ExperimentDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExperimentTaskType>> GetAllAsync()
        => await _context.ExperimentTaskTypes.ToListAsync();

    public async Task<ExperimentTaskType?> GetByIdAsync(Guid id)
        => await _context.ExperimentTaskTypes.FindAsync(id);

    public async Task AddAsync(ExperimentTaskType entity)
        => await _context.ExperimentTaskTypes.AddAsync(entity);

    public void Update(ExperimentTaskType entity)
        => _context.ExperimentTaskTypes.Update(entity);

    public void Delete(ExperimentTaskType entity)
        => _context.ExperimentTaskTypes.Remove(entity);
}