using App.Modules.Identity.Application.Interfaces;
using App.Modules.Identity.Applications.Interfaces;
using App.Modules.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Identity.Infrastructure.Repositories;

internal sealed class AppRefreshTokenRepository : IAppRefreshTokenRepository
{
    private readonly IdentityModuleDbContext _context;

    public AppRefreshTokenRepository(IdentityModuleDbContext context)
    {
        _context = context;
    }
    public async Task<AppRefreshToken?> GetValidTokenAsync(string tokenHash, Guid userId)
    {
        return await _context.AppRefreshTokens
            .SingleOrDefaultAsync(x =>
                x.TokenHash == tokenHash &&
                x.UserId == userId &&
                !x.IsRevoked &&
                x.ExpiresAt > DateTime.UtcNow
            );
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
        var expired = await _context.AppRefreshTokens
            .Where(t => t.UserId == userId && t.ExpiresAt <= DateTime.UtcNow)
            .ToListAsync();

        _context.AppRefreshTokens.RemoveRange(expired);
        return expired.Count;
    }
}