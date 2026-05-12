using App.Domain.Entities;
using App.Modules.Identity.Applications.Interfaces;
using App.Modules.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Equipment.Infrastructure.Repositories;

internal sealed class InstituteUserRepository : IInstituteUserRepository
{
    private readonly IdentityModuleDbContext _context;

    public InstituteUserRepository(IdentityModuleDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InstituteUser>> GetAllAsync()
        => await _context.InstituteUsers.ToListAsync();

    public async Task<InstituteUser?> GetByIdAsync(Guid id)
        => await _context.InstituteUsers.FindAsync(id);

    public async Task AddAsync(InstituteUser entity)
        => await _context.InstituteUsers.AddAsync(entity);

    public void Update(InstituteUser entity)
        => _context.InstituteUsers.Update(entity);

    public void Delete(InstituteUser entity)
        => _context.InstituteUsers.Remove(entity);
}