using App.Modules.Project.Application.DTO;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Project.Application.Mappers;

public static class ProjectMapper
{
    // Entity → List Response
    public static ProjectListResponse ToListResponse(Domain.Project entity)
        => new ()
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName.Translate() ?? String.Empty,
            Funding = entity.Funding,
            ProjectTypeName = entity.ProjectType.Name.Translate() ?? String.Empty,
            CreatedAt = entity.CreatedAt,
        };

    // Entity → Full Response
    public static ProjectResponse ToResponse(Domain.Project entity)
        => new ()
        {
            Id = entity.Id,
            ProjectTypeName = entity.ProjectType.Name.Translate() ?? String.Empty,
            ProjectName = entity.ProjectName.Translate() ?? String.Empty,
            Funding = entity.Funding,
            Requirements = entity.Requirements?.Translate() ?? String.Empty,
            RequirementsFilePath = entity.RequirementsFilePath
        };

    // Create Request → Entity
    public static Domain.Project ToEntity(SaveProjectRequest request)
        => new ()
        {
            ProjectTypeId = request.ProjectTypeId,
            ProjectName = new LangStr()
            {
              [Cultures.Estonian] = request.ProjectNameEn,
              [Cultures.English] = request.ProjectNameEt,
            },
            Funding = request.Funding,
            Requirements = new LangStr()
            {
            [Cultures.Estonian] = request.RequirementsEt ?? String.Empty,
            [Cultures.English] = request.RequirementsEn ?? String.Empty,
        },
            RequirementsFilePath = request.RequirementsFilePath
        };
    public static SaveProjectRequest ToRequest(Domain.Project entity) => new()
    {
        ProjectTypeId = entity.ProjectTypeId,
        ProjectNameEn = entity.ProjectName[Cultures.English],
        ProjectNameEt = entity.ProjectName[Cultures.Estonian],
        Funding = entity.Funding,
        RequirementsEn = entity.Requirements?[Cultures.English],
        RequirementsEt = entity.Requirements?[Cultures.Estonian],
        RequirementsFilePath = entity.RequirementsFilePath
    };

    public static void UpdateEntity(Domain.Project entity, SaveProjectRequest request)
    {
        entity.ProjectName.SetTranslation(request.ProjectNameEt, Cultures.Estonian);
        entity.ProjectName.SetTranslation(request.ProjectNameEn, Cultures.English);

        entity.Requirements ??= new LangStr();
        entity.Requirements.SetTranslation(request.RequirementsEt ?? String.Empty, Cultures.Estonian);
        entity.Requirements.SetTranslation(request.RequirementsEn ?? String.Empty, Cultures.English);
        
        entity.Funding = request.Funding;
        entity.RequirementsFilePath = request.RequirementsFilePath;
        entity.ProjectTypeId = request.ProjectTypeId;
    }
}