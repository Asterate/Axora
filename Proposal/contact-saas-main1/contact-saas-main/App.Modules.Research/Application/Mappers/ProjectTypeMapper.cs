using App.Domain.Entities;

namespace App.Modules.Project.Application.Mapper;

public static class ProjectTypeMapper
{
    // Entity → List Response
    public static ProjectTypeListResponse ToListResponse(ProjectType entity)
        => new ProjectTypeListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static ProjectTypeResponse ToResponse(ProjectType entity) => new()
    {
        Id = entity.Id,
        NameEn = entity.Name.Translate("en"),
        NameEt = entity.Name.Translate("et"),
        DescriptionEn = entity.Description?.Translate("en"),
        DescriptionEt = entity.Description?.Translate("et")
    };

    // Create Request → Entity
    public static ProjectType ToEntity(CreateProjectTypeRequest request)
        => new ProjectType
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ProjectType entity, UpdateProjectTypeRequest request)
    {
        entity.Id = request.Id;
    }
}