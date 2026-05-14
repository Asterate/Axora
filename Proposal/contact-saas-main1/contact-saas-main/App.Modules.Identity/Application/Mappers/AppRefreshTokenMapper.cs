using App.Modules.Identity.Application.DTO;
using App.Modules.Identity.Domain;

namespace App.Modules.Identity.Application.Mappers;

public static class AppRefreshTokenMapper
{
    public static AppRefreshTokenResponse ToResponse(AppRefreshToken entity)
        => new()
        {
            Id = entity.Id,
            ExpiresAt = entity.ExpiresAt,
            UserId = entity.UserId
        };

    public static AppRefreshToken ToEntity(CreateAppRefreshTokenRequest request)
        => new()
        {
            UserId = request.UserId,
            DeviceInfo = request.DeviceInfo,
            IpAddress = request.IpAddress,
            UserAgent = request.UserAgent
        };
}