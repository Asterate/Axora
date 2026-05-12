using App.Domain.Identity;
using App.Shared.Contracts;

namespace App.Modules.Identity.Applications.Interfaces;

public interface IAppRefreshTokenRepository : IBaseRepository<AppRefreshToken>
{
    Task<int> DeleteExpiredByUserIdAsync(Guid userId);
}