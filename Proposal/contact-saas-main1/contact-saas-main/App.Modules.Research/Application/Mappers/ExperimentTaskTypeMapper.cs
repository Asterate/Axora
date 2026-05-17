using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Project.Application.Mappers;

public static class ExperimentTaskTypeMapper
{
    // Entity → List Response
    public static ExperimentTaskTypeResponse ToResponse(ExperimentTaskType entity)
        => new ()
        {
            Id = entity.Id,
            Name = entity.Name.Translate(),
            Description = entity.Description?.Translate(),
        };

    // Create Request → Entity
    public static ExperimentTaskType ToEntity(SaveExperimentTaskTypeRequest request)
        => new ()
        {
            Name = new LangStr()
            {
                [Cultures.Estonian] = request.NameEt,
                [Cultures.English] = request.NameEn,
            },
            Description = new LangStr()
            {
                [Cultures.Estonian] = request.DescriptionEt ?? String.Empty,
                [Cultures.English] = request.DescriptionEn ?? String.Empty,
            }
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ExperimentTaskType entity, SaveExperimentTaskTypeRequest request)
    {
        entity.Name.SetTranslation(request.NameEt ?? String.Empty, Cultures.Estonian);
        entity.Name.SetTranslation(request.NameEn ?? String.Empty, Cultures.Estonian);
    }
}