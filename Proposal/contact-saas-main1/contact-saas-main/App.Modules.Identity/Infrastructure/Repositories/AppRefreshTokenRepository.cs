using App.Domain.Identity;
using App.Modules.Identity.Applications.Interfaces;
using App.Modules.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Equipment.Infrastructure.Repositories;

internal sealed class AppRefreshTokenRepository : IAppRefreshTokenRepository
{
    private readonly IdentityDbContext _context;

    public AppRefreshTokenRepository(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AppRefreshToken>> GetAllAsync()
        => await _context.AppRefreshToken.ToListAsync();

    public async Task<AppRefreshToken?> GetByIdAsync(Guid id)
        => await _context.AppRefreshToken.FindAsync(id);

    public async Task AddAsync(AppRefreshToken entity)
        => await _context.AppRefreshToken.AddAsync(entity);

    public void Update(AppRefreshToken entity)
        => _context.AppRefreshToken.Update(entity);

    public void Delete(AppRefreshToken entity)
        => _context.AppRefreshToken.Remove(entity);
}