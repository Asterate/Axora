using App.Domain.Entities;
using App.Modules.Experiment.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Experiment.Infrastructure.Repositories;

internal sealed class ExperimentTypeRepository : IExperimentTypeRepository
{
    private readonly ExperimentDbContext _context;

    public ExperimentTypeRepository(ExperimentDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExperimentType>> GetAllAsync()
        => await _context.ExperimentTypes.ToListAsync();

    public async Task<ExperimentType?> GetByIdAsync(Guid id)
        => await _context.ExperimentTypes.FindAsync(id);

    public async Task AddAsync(ExperimentType entity)
        => await _context.ExperimentTypes.AddAsync(entity);

    public void Update(ExperimentType entity)
        => _context.ExperimentTypes.Update(entity);

    public void Delete(ExperimentType entity)
        => _context.ExperimentTypes.Remove(entity);
}