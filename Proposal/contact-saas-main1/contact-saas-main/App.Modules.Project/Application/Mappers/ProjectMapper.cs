namespace App.Modules.Project.Application.Mapper;

public static class ProjectMapper
{
    // Entity → List Response
    public static ProjectListResponse ToListResponse(Domain.Entities.Project entity)
        => new ProjectListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static ProjectResponse ToResponse(Domain.Entities.Project entity)
        => new ProjectResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static Domain.Entities.Project ToEntity(CreateProjectRequest request)
        => new Domain.Entities.Project
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Domain.Entities.Project entity, UpdateProjectRequest request)
    {
        entity.Id = request.Id;
    }
}