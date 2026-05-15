using App.Modules.Project.Application.DTO;

namespace App.Modules.Project.Application.Mappers;

public static class ProjectMapper
{
    // Entity → List Response
    public static ProjectListResponse ToListResponse(Domain.Project entity)
        => new ()
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            Funding = entity.Funding,
            ProjectTypeName = entity.ProjectType.Name,
            CreatedAt = entity.CreatedAt,
        };

    // Entity → Full Response
    public static ProjectResponse ToResponse(Domain.Project entity)
        => new ()
        {
            Id = entity.Id,
            ProjectTypeId = entity.ProjectTypeId,
            ProjectName = entity.ProjectName,
            Funding = entity.Funding,
            Requirements = entity.Requirements,
            RequirementsFilePath = entity.RequirementsFilePath
        };

    // Create Request → Entity
    public static Domain.Project ToEntity(CreateProjectRequest request)
        => new ()
        {
            ProjectTypeId = request.ProjectTypeId,
            ProjectName = request.ProjectName,
            Funding = request.Funding,
            Requirements = request.Requirements,
            RequirementsFilePath = request.RequirementsFilePath
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Domain.Project entity, UpdateProjectRequest request)
    {
        entity.Id = request.Id;
        entity.ProjectName = request.ProjectName;
        entity.Funding = request.Funding;
        entity.Requirements = request.Requirements;
        entity.RequirementsFilePath = request.RequirementsFilePath;
        entity.ProjectTypeId = request.ProjectTypeId;
    }
}