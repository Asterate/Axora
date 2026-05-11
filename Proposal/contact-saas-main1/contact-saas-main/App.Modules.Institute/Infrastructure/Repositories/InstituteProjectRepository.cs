using App.Domain.Entities;
using App.Modules.Institute.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Institute.Infrastructure.Repositories;

internal sealed class InstituteProjectRepository : IInstituteProjectRepository
{
    private readonly InstituteDbContext _context;

    public InstituteProjectRepository(InstituteDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InstituteProject>> GetAllAsync()
        => await _context.InstituteProjects.ToListAsync();

    public async Task<InstituteProject?> GetByIdAsync(Guid id)
        => await _context.InstituteProjects.FindAsync(id);

    public async Task AddAsync(InstituteProject entity)
        => await _context.InstituteProjects.AddAsync(entity);

    public void Update(InstituteProject entity)
        => _context.InstituteProjects.Update(entity);

    public void Delete(InstituteProject entity)
        => _context.InstituteProjects.Remove(entity);
}