using App.Domain.Identity;
using App.Modules.Identity.Applications.Interfaces;
using App.Modules.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Equipment.Infrastructure.Repositories;

internal sealed class AppRefreshTokenRepository : IAppRefreshTokenRepository
{
    private readonly IdentityModuleDbContext _context;

    public AppRefreshTokenRepository(IdentityModuleDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AppRefreshToken>> GetAllAsync()
        => await _context.AppRefreshTokens.ToListAsync();

    public async Task<AppRefreshToken?> GetByIdAsync(Guid id)
        => await _context.AppRefreshTokens.FindAsync(id);

    public async Task AddAsync(AppRefreshToken entity)
        => await _context.AppRefreshTokens.AddAsync(entity);

    public void Update(AppRefreshToken entity)
        => _context.AppRefreshTokens.Update(entity);

    public void Delete(AppRefreshToken entity)
        => _context.AppRefreshTokens.Remove(entity);

    public async Task<int> DeleteExpiredByUserIdAsync(Guid userId)
    {
        if (_context.Database.ProviderName!.Contains("InMemory"))
        {
            var expired = _context.AppRefreshTokens
                .Where(t => t.UserId == userId && t.Expiration < DateTime.UtcNow)
                .ToList();

            _context.AppRefreshTokens.RemoveRange(expired);
            return expired.Count;
        }

        return await _context.AppRefreshTokens
            .Where(t => t.UserId == userId && t.Expiration < DateTime.UtcNow)
            .ExecuteDeleteAsync();
    }
}