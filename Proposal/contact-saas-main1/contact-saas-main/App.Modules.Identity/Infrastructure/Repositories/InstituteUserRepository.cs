using App.Domain.Entities;
using App.Modules.Identity.Applications.Interfaces;
using App.Modules.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Equipment.Infrastructure.Repositories;

internal sealed class InstituteUserRepository : IInstituteUserRepository
{
    private readonly IdentityDbContext _context;

    public InstituteUserRepository(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InstituteUser>> GetAllAsync()
        => await _context.InstituteUser.ToListAsync();

    public async Task<InstituteUser?> GetByIdAsync(Guid id)
        => await _context.InstituteUser.FindAsync(id);

    public async Task AddAsync(InstituteUser entity)
        => await _context.InstituteUser.AddAsync(entity);

    public void Update(InstituteUser entity)
        => _context.InstituteUser.Update(entity);

    public void Delete(InstituteUser entity)
        => _context.InstituteUser.Remove(entity);
}