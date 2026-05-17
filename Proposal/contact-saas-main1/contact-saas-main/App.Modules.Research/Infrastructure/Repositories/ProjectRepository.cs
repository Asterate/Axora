using App.Modules.Project.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Project.Infrastructure.Repositories;

internal sealed class ProjectRepository : IProjectRepository
{
    private readonly ResearchDbContext _context;

    public ProjectRepository(ResearchDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Project>> GetAllAsync()
        => await _context.Projects
            .Include(p => p.ProjectType)
            .ToListAsync();

    public async Task<Domain.Project?> GetByIdAsync(Guid id)
        => await _context.Projects
            .Include(p => p.ProjectType) // ← missing
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task AddAsync(Domain.Project entity)
        => await _context.Projects.AddAsync(entity);

    public void Update(Domain.Project entity)
        => _context.Projects.Update(entity);

    public void Delete(Domain.Project entity)
        => _context.Projects.Remove(entity);
}