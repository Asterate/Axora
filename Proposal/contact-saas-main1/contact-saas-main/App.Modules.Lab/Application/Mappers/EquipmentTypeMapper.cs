using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Domain;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Lab.Application.Mappers;

public static class EquipmentTypeMapper
{
    // Entity → List Response
    public static EquipmentTypeResponse ToResponse(EquipmentType entity)
        => new ()
        {
            Id = entity.Id,
            Name = entity.Name.Translate() ?? "??",
            Description = entity.Description?.Translate() ?? "??",
        };

    // Create Request → Entity
    public static EquipmentType ToEntity(SaveEquipmentTypeRequest request)
        => new ()
        {
            Name = new LangStr{
                [Cultures.English] = request.NameEn ?? "??",
                [Cultures.Estonian] =  request.NameEt ?? "??"
                },
            Description = new LangStr{
                [Cultures.English] = request.DescriptionEn ?? "??",
                [Cultures.Estonian] =  request.DescriptionEt ?? "??"
            }
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(EquipmentType entity, SaveEquipmentTypeRequest request)
    {
        entity.Name.SetTranslation(request.NameEn ?? "??", Cultures.English);
        entity.Name.SetTranslation(request.NameEt ?? "??", Cultures.Estonian);
        entity.Description ??= new LangStr();
        entity.Description.SetTranslation(request.DescriptionEt ?? "??", Cultures.Estonian);
        entity.Description.SetTranslation(request.DescriptionEn ?? "??", Cultures.English);
    }
}