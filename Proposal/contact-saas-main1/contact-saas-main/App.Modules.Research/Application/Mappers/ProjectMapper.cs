namespace App.Modules.Project.Application.Mapper;

public static class ProjectMapper
{
    // Entity → List Response
    public static ProjectListResponse ToListResponse(Domain.Entities.Project entity)
        => new ()
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static ProjectResponse ToResponse(Domain.Entities.Project entity)
        => new ()
        {
            Id = entity.Id,
            ProjectTypeId = entity.ProjectTypeId,  // ← Missing!
            ProjectName = entity.ProjectName ?? string.Empty,
            Funding = entity.Funding,
            Requirements = entity.Requirements,
            RequirementsFilePath = entity.RequirementsFilePath
        };

    // Create Request → Entity
    public static Domain.Entities.Project ToEntity(CreateProjectRequest request)
        => new Domain.Entities.Project
        {
            Id = request.Id,
            ProjectTypeId = request.ProjectTypeId,  // ← Missing!
            ProjectName = request.ProjectName ?? string.Empty,
            Funding = request.Funding,
            Requirements = request.Requirements,
            RequirementsFilePath = request.RequirementsFilePath
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Domain.Entities.Project entity, UpdateProjectRequest request)
    {
        entity.ProjectName = request.ProjectName ?? string.Empty;
        entity.Funding = request.Funding;
        entity.Requirements = request.Requirements;
        entity.RequirementsFilePath = request.RequirementsFilePath;
        entity.ProjectTypeId = request.ProjectTypeId;
    }
}