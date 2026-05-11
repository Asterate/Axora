using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Lab.Infrastructure.Repositories;

internal sealed class ProjectRepository : IProjectRepository
{
    private readonly ProjectDbContext _context;

    public ProjectRepository(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Entities.Project>> GetAllAsync()
        => await _context.Projects.ToListAsync();

    public async Task<Domain.Entities.Project?> GetByIdAsync(Guid id)
        => await _context.Projects.FindAsync(id);

    public async Task AddAsync(Domain.Entities.Project entity)
        => await _context.Projects.AddAsync(entity);

    public void Update(Domain.Entities.Project entity)
        => _context.Projects.Update(entity);

    public void Delete(Domain.Entities.Project entity)
        => _context.Projects.Remove(entity);
}