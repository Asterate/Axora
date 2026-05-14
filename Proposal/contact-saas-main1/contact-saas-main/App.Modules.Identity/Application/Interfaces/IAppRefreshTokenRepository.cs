using App.Modules.Identity.Domain;
using App.Shared.Contracts;

namespace App.Modules.Identity.Application.Interfaces;

public interface IAppRefreshTokenRepository : IBaseRepository<AppRefreshToken>
{
    Task<int> DeleteExpiredByUserIdAsync(Guid userId);
    Task<AppRefreshToken?> GetValidTokenAsync(string tokenHash, Guid userId);
}