using App.Domain.Entities;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Lab.Infrastructure.Repositories;

internal sealed class ProjectTypeRepository : IProjectTypeRepository
{
    private readonly ProjectDbContext _context;

    public ProjectTypeRepository(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectType>> GetAllAsync()
        => await _context.ProjectTypes.ToListAsync();

    public async Task<ProjectType?> GetByIdAsync(Guid id)
        => await _context.ProjectTypes.FindAsync(id);

    public async Task AddAsync(ProjectType entity)
        => await _context.ProjectTypes.AddAsync(entity);

    public void Update(ProjectType entity)
        => _context.ProjectTypes.Update(entity);

    public void Delete(ProjectType entity)
        => _context.ProjectTypes.Remove(entity);
}