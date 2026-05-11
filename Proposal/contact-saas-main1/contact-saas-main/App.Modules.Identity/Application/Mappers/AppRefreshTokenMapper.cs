using App.Domain.Identity;
using App.Shared.Domain;

namespace App.Modules.Equipment.Application.Mapper;

public static class AppRefreshTokenMapper
{
    // Entity → List Response
    public static AppRefreshTokenListResponse ToListResponse(AppRefreshToken entity)
        => new AppRefreshTokenListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static AppRefreshTokenResponse ToResponse(AppRefreshToken entity)
        => new AppRefreshTokenResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static AppRefreshToken ToEntity(CreateAppRefreshTokenRequest request)
        => new AppRefreshToken
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(AppRefreshToken entity, UpdateAppRefreshTokenRequest request)
    {
        entity.Id = request.Id;
    }
}