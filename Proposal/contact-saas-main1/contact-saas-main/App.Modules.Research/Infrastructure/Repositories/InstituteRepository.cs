using App.Modules.Institute.Application.Interfaces;
using App.Modules.Project.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Institute.Infrastructure.Repositories;

internal sealed class InstituteRepository : IInstituteRepository
{
    private readonly ResearchDbContext _context;

    public InstituteRepository(ResearchDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Entities.Institute>> GetAllAsync()
        => await _context.Institutes.ToListAsync();

    public async Task<Domain.Entities.Institute?> GetByIdAsync(Guid id)
        => await _context.Institutes.FindAsync(id);
    

    public async Task AddAsync(Domain.Entities.Institute entity)
        => await _context.Institutes.AddAsync(entity);

    public void Update(Domain.Entities.Institute entity)
        => _context.Institutes.Update(entity);

    public void Delete(Domain.Entities.Institute entity)
        => _context.Institutes.Remove(entity);
    
    public async Task<List<Domain.Entities.Institute>> GetActivesAsync()
    {
        return await _context.Institutes
            .Where(i => i.Active && i.DeletedAt == null)
            .OrderBy(i => i.InstituteName)
            .ToListAsync();
    }
}