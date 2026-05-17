using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Project.Application.Mappers;

public static class ExperimentTypeMapper
{
    // Entity → List Response
    public static ExperimentTypeResponse ToResponse(ExperimentType entity)
        => new ()
        {
            Id = entity.Id,
            Name = entity.Name.Translate(),
            Description = entity.Description?.Translate()
        };

    // Create Request → Entity
    public static ExperimentType ToEntity(SaveExperimentTypeRequest request)
        => new ()
        {
            Name = new LangStr()
            {
                [Cultures.English] =  request.NameEn,
                [Cultures.Estonian] =   request.NameEt,
            },
            Description = new LangStr()
            {
                [Cultures.English] =  request.DescriptionEn ?? String.Empty,
                [Cultures.Estonian] =   request.DescriptionEt ?? String.Empty,
            }
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ExperimentType entity, SaveExperimentTypeRequest request)
    {
        entity.Name.SetTranslation(request.NameEn, Cultures.English);
        entity.Description ??= new LangStr();
        entity.Description.SetTranslation(request.DescriptionEt ?? String.Empty, Cultures.Estonian);
        entity.Description.SetTranslation(request.DescriptionEn ?? String.Empty, Cultures.English);
    }
    public static SaveExperimentTypeRequest ToUpdateRequest(ExperimentType entity)
    {
        return new SaveExperimentTypeRequest
        {
            NameEn = entity.Name.Translate("en") ?? String.Empty,
            NameEt = entity.Name.Translate("et") ?? String.Empty,
            DescriptionEn = entity.Description?.Translate("en"),
            DescriptionEt = entity.Description?.Translate("et")
        };
    }
}