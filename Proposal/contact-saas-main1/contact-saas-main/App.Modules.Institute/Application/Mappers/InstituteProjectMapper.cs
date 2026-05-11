using App.Domain.Entities;

namespace App.Modules.Institute.Application.Mapper;

public static class InstituteProjectMapper
{
    // Entity → List Response
    public static InstituteProjectListResponse ToListResponse(InstituteProject entity)
        => new InstituteProjectListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static InstituteProjectResponse ToResponse(InstituteProject entity)
        => new InstituteProjectResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static InstituteProject ToEntity(CreateInstituteProjectRequest request)
        => new InstituteProject
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(InstituteProject entity, UpdateInstituteProjectRequest request)
    {
        request.Id = entity.Id;
    }
}