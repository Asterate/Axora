using App.Modules.Institute.Application.Interfaces;
using App.Modules.Project.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Project.Infrastructure.Repositories;

internal sealed class InstituteTypeRepository : IInstituteTypeRepository
{
    private readonly ResearchDbContext _context;

    public InstituteTypeRepository(ResearchDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InstituteType>> GetAllAsync()
        => await _context.InstituteTypes.Where(x => x.DeletedAt != null).ToListAsync();

    public async Task<InstituteType?> GetByIdAsync(Guid id)
        => await _context.InstituteTypes.FindAsync(id);

    public async Task AddAsync(InstituteType entity)
        => await _context.InstituteTypes.AddAsync(entity);

    public void Update(InstituteType entity)
        => _context.InstituteTypes.Update(entity);

    public void Delete(InstituteType entity)
        => _context.InstituteTypes.Remove(entity);
    
    public async Task<List<InstituteType>> GetActivesAsync()
    {
        return await _context.InstituteTypes.ToListAsync();
    }
}