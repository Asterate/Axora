using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Domain;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Lab.Application.Mappers;

public static class LabTypeMapper
{
    // Entity → List Response
    public static LabTypeResponse ToResponse(LabType entity)
        => new ()
        {
            Id = entity.Id,
            Name = entity.Name.Translate(),
            Description = entity.Description?.Translate() ?? "??"
        };

    // Create Request → Entity
    public static LabType ToEntity(SaveLabTypeRequest request)
        => new ()
        {
            Name = new LangStr
            {
                [Cultures.English] = request.NameEn ?? "",
                [Cultures.Estonian] = request.NameEt ?? ""
            },
            Description = new LangStr
            {
                [Cultures.English] = request.DescriptionEn ?? "",
                [Cultures.Estonian] = request.DescriptionEt ?? ""
            }
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(LabType entity, SaveLabTypeRequest request)
    {
        entity.Name.SetTranslation(request.NameEn ?? "", Cultures.English);
        entity.Name.SetTranslation(request.NameEt ?? "", Cultures.Estonian);
        entity.Description ??= new LangStr();
        entity.Description.SetTranslation(request.DescriptionEn ?? "", Cultures.English);
        entity.Description.SetTranslation(request.DescriptionEt ?? "", Cultures.Estonian);
    }
}