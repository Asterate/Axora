using App.Domain.Entities;
using App.Modules.Institute.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Institute.Infrastructure.Repositories;

internal sealed class InstituteTypeRepository : IInstituteTypeRepository
{
    private readonly InstituteDbContext _context;

    public InstituteTypeRepository(InstituteDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InstituteType>> GetAllAsync()
        => await _context.InstituteTypes.ToListAsync();

    public async Task<InstituteType?> GetByIdAsync(Guid id)
        => await _context.InstituteTypes.FindAsync(id);

    public async Task AddAsync(InstituteType entity)
        => await _context.InstituteTypes.AddAsync(entity);

    public void Update(InstituteType entity)
        => _context.InstituteTypes.Update(entity);

    public void Delete(InstituteType entity)
        => _context.InstituteTypes.Remove(entity);
}