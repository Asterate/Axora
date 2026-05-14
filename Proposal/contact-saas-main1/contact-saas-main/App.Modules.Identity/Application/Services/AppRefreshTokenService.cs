using System.Security.Cryptography;
using System.Text;
using App.Modules.Identity.Application.DTO;
using App.Modules.Identity.Application.Interfaces;
using App.Modules.Identity.Application.Mappers;
using App.Modules.Identity.Domain;
using App.Shared.Contracts;

namespace App.Modules.Identity.Application.Services;

public sealed class AppRefreshTokenService : IAppRefreshTokenService
{
    private static readonly TimeSpan RefreshTokenLifetime = TimeSpan.FromDays(7);

    private readonly IAppRefreshTokenRepository _repo;
    private readonly IUnitOfWork _uow;

    public AppRefreshTokenService(IAppRefreshTokenRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<IEnumerable<AppRefreshTokenResponse>> GetAllAsync()
    {
        var entities = await _repo.GetAllAsync();
        return entities.Select(AppRefreshTokenMapper.ToResponse);
    }

    public async Task<AppRefreshTokenResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id);
        return entity is null ? null : AppRefreshTokenMapper.ToResponse(entity);
    }

    public async Task<AppRefreshTokenResponse> CreateAsync(CreateAppRefreshTokenRequest request)
    {
        var plainToken = GenerateSecureToken();
        var tokenHash = HashToken(plainToken);

        var entity = AppRefreshTokenMapper.ToEntity(request);
        entity.TokenHash = tokenHash;
        entity.ExpiresAt = DateTime.UtcNow + RefreshTokenLifetime;
        entity.IsRevoked = false;
        entity.ExpiresAt = request.ExpiresAt ?? DateTime.UtcNow + RefreshTokenLifetime;

        await _repo.AddAsync(entity);
        await _uow.SaveChangesAsync();

        var response = AppRefreshTokenMapper.ToResponse(entity);
        response.RefreshToken = plainToken;

        return response;
    }
    


    public async Task RevokeAsync(string plaintextToken, Guid userId, string reason = "logout")
    {
        var tokenHash = HashToken(plaintextToken);
        var entity = await _repo.GetValidTokenAsync(tokenHash, userId);
        if (entity is null) return;

        entity.IsRevoked = true;
        entity.RevokedAt = DateTime.UtcNow;
        entity.RevocationReason = reason;

        _repo.Update(entity);
        await _uow.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) return;

        _repo.Delete(entity);
        await _uow.SaveChangesAsync();
    }

    public Task<int> DeleteExpiredByUserIdAsync(Guid userId)
        => _repo.DeleteExpiredByUserIdAsync(userId);

    private static string GenerateSecureToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }

    private static string HashToken(string token)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToHexString(bytes);
    }
    public async Task<AppRefreshTokenResponse?> ValidateAndRotateAsync(
        string plaintextToken,
        Guid userId,
        DateTime expiresAt,
        string? ipAddress = null,
        string? userAgent = null)
    {
        var now = DateTime.UtcNow;
        var tokenHash = HashToken(plaintextToken);

        var existing = await GetValidExistingTokenAsync(tokenHash, userId);
        if (existing is null)
            return null;

        RevokeToken(existing, now);

        var newPlainToken = GenerateSecureToken();
        var newHash = HashToken(newPlainToken);

        existing.ReplacedByTokenHash = newHash;

        var newToken = CreateNewToken(
            userId,
            newHash,
            expiresAt,
            existing,
            ipAddress,
            userAgent);

        _repo.Update(existing);
        await _repo.AddAsync(newToken);
        await _uow.SaveChangesAsync();

        return BuildResponse(newToken, newPlainToken);
    }

    private async Task<AppRefreshToken?> GetValidExistingTokenAsync(string tokenHash, Guid userId)
    {
        return await _repo.GetValidTokenAsync(tokenHash, userId);
    }

    private static void RevokeToken(AppRefreshToken token, DateTime now)
    {
        token.LastUsedAt = now;
        token.IsRevoked = true;
        token.RevokedAt = now;
        token.RevocationReason = "rotation";
    }

    private static AppRefreshToken CreateNewToken(
        Guid userId,
        string tokenHash,
        DateTime expiresAt,
        AppRefreshToken existing,
        string? ipAddress,
        string? userAgent)
    {
        return new AppRefreshToken
        {
            UserId = userId,
            TokenHash = tokenHash,
            ExpiresAt = expiresAt,
            IsRevoked = false,
            DeviceInfo = existing.DeviceInfo,
            IpAddress = ipAddress ?? existing.IpAddress,
            UserAgent = userAgent ?? existing.UserAgent
        };
    }

    private static AppRefreshTokenResponse BuildResponse(AppRefreshToken token, string plainToken)
    {
        return new AppRefreshTokenResponse
        {
            Id = token.Id,
            RefreshToken = plainToken,
            ExpiresAt = token.ExpiresAt,
            UserId = token.UserId
        };
    }
}