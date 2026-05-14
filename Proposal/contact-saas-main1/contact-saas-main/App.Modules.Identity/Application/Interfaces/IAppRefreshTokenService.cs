using App.Modules.Identity.Application.DTO;

namespace App.Modules.Identity.Application.Interfaces;

public interface IAppRefreshTokenService
{
    Task<IEnumerable<AppRefreshTokenResponse>> GetAllAsync();
    Task<AppRefreshTokenResponse?> GetByIdAsync(Guid id);
    Task<AppRefreshTokenResponse> CreateAsync(CreateAppRefreshTokenRequest request);
    Task RevokeAsync(string plaintextToken, Guid userId, string reason = "logout");
    Task DeleteAsync(Guid id);
    Task<int> DeleteExpiredByUserIdAsync(Guid userId);

    Task<AppRefreshTokenResponse?> ValidateAndRotateAsync(
        string plaintextToken,
        Guid userId,
        DateTime expiresAt,
        string? ipAddress = null,
        string? userAgent = null);
}