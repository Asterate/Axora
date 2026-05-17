using App.Modules.Experiment.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Project.Infrastructure.Repositories;

internal sealed class ExperimentRepository : IExperimentRepository
{
    private readonly ResearchDbContext _context;

    public ExperimentRepository(ResearchDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Project.Domain.Experiment>> GetAllAsync()
        => await _context.Experiments.Include(e => e.ExperimentType)  // ← add
            .Include(e => e.Projects).ToListAsync();

    public async Task<Project.Domain.Experiment?> GetByIdAsync(Guid id)
        => await _context.Experiments
            .Include(e => e.ExperimentType)
            .Include(e => e.Projects)
            .FirstOrDefaultAsync(e => e.Id == id);

    public async Task AddAsync(Project.Domain.Experiment entity)
        => await _context.Experiments.AddAsync(entity);

    public void Update(Project.Domain.Experiment entity)
        => _context.Experiments.Update(entity);

    public void Delete(Project.Domain.Experiment entity)
        => _context.Experiments.Remove(entity);
}