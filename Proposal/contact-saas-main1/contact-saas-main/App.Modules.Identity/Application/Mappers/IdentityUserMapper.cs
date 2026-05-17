using App.Modules.Identity.Application.DTO;
using App.Modules.Identity.Domain;

namespace App.Modules.Identity.Application.Mappers;

public static class InstituteUserMapper
{
    // Entity → List Response
    public static InstituteUserListResponse ToListResponse(InstituteUser entity)
        => new ()
        {
            Id = entity.Id,
            Role =  entity.Role,
        };

    // Entity → Full Response
    public static InstituteUserResponse ToResponse(InstituteUser entity)
        => new ()
        {
            Id = entity.Id,
            Role = entity.Role,
            
        };

    // Create Request → Entity
    public static InstituteUser ToEntity(SaveInstituteUserRequest request)
        => new ()
        {
            UserId = request.UserId,
            InstituteId = request.InstituteId,
            Role = request.Role,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(InstituteUser entity, SaveInstituteUserRequest request)
    {
        entity.UserId = request.UserId;
        entity.InstituteId = request.InstituteId;
        entity.Role = request.Role;
        entity.UpdatedAt = DateTime.UtcNow;
    }
    public static SaveInstituteUserRequest ToUpdateRequest(InstituteUser entity)
    {
        return new SaveInstituteUserRequest
        {
            UserId = entity.UserId,
            InstituteId = entity.InstituteId,
            Role = entity.Role
        };
    }
}