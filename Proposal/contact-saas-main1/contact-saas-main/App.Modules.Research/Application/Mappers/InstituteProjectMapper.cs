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
    public static InstituteProject ToEntity(SaveInstituteProjectRequest request)
        => new ()
        {
            InstituteId = request.InstituteId,
            ProjectId = request.ProjectId
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(InstituteProject entity, SaveInstituteProjectRequest request)
    {
        entity.InstituteId = request.InstituteId;
        entity.ProjectId = request.ProjectId;
    }
    public static SaveInstituteProjectRequest ToUpdateRequest(InstituteProject request)
    {
        return new SaveInstituteProjectRequest
        {
            InstituteId = request.InstituteId,
            ProjectId = request.ProjectId,
            
        };
    }
}