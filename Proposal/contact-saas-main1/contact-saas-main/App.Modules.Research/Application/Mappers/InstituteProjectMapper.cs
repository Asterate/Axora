using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;
using App.Shared.Domain;

namespace App.Modules.Project.Application.Mappers;

public static class InstituteProjectMapper
{
    // Entity → Full Response
    public static InstituteProjectResponse ToResponse(InstituteProject entity)
        => new ()
        {
            Id = entity.Id,
            InstituteName = entity.Institute.InstituteName,
            ProjectId = entity.ProjectId
        };

    // Create Request → Entity
    public static InstituteProject ToEntity(CreateInstituteProjectRequest request)
        => new ()
        {
            InstituteId = request.InstituteId,
            ProjectId = request.ProjectId
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(InstituteProject entity, UpdateInstituteProjectRequest request)
    {
        entity.Id = request.Id;
        entity.InstituteId = request.InstituteId;
        entity.ProjectId = request.ProjectId;
    }
    public static UpdateInstituteProjectRequest ToUpdateRequest(InstituteProject request)
    {
        return new UpdateInstituteProjectRequest
        {
            Id = request.Id,
            InstituteId = request.InstituteId,
            ProjectId = request.ProjectId,
            
        };
    }
}