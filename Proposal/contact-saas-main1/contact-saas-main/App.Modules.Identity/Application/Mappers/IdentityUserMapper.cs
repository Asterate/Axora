using App.Domain.Entities;

namespace App.Modules.Equipment.Application.Mapper;

public static class InstituteUserMapper
{
    // Entity → List Response
    public static InstituteUserListResponse ToListResponse(InstituteUser entity)
        => new InstituteUserListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static InstituteUserResponse ToResponse(InstituteUser entity)
        => new InstituteUserResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static InstituteUser ToEntity(CreateInstituteUserRequest request)
        => new InstituteUser
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(InstituteUser entity, UpdateInstituteUserRequest request)
    {
        entity.Id = request.Id;
    }
}