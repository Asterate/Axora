using App.Modules.Experiment.Application.Interfaces;
using App.Modules.Project.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Experiment.Infrastructure.Repositories;

internal sealed class ExperimentRepository : IExperimentRepository
{
    private readonly ResearchDbContext _context;

    public ExperimentRepository(ResearchDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Entities.Experiment>> GetAllAsync()
        => await _context.Experiments.ToListAsync();

    public async Task<Domain.Entities.Experiment?> GetByIdAsync(Guid id)
        => await _context.Experiments.FindAsync(id);

    public async Task AddAsync(Domain.Entities.Experiment entity)
        => await _context.Experiments.AddAsync(entity);

    public void Update(Domain.Entities.Experiment entity)
        => _context.Experiments.Update(entity);

    public void Delete(Domain.Entities.Experiment entity)
        => _context.Experiments.Remove(entity);
}