using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Domain;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Lab.Application.Mappers;

public class ReagentTypeMapper
{
    // Entity → List Response
    public static ReagentTypeListResponse ToListResponse(ReagentType entity)
        => new ()
        {
            Id = entity.Id,
            Name = entity.Name.Translate() ?? "??",
            Description = entity.Description?.Translate() ?? "??",
            Category =  entity.Category?.Translate() ?? "??",
            HazardLevel = entity.HazardLevel?.Translate() ?? "??",
            IsHazardous = entity.IsHazardous,
            ColorCode = entity.ColorCode?.Translate() ?? "??",
        };

    // Entity → Full Response
    public static ReagentTypeResponse ToResponse(ReagentType entity)
        => new ()
        {
            Id = entity.Id,
            Name = entity.Name.Translate() ?? "??",
            Description = entity.Description?.Translate() ?? "??",
            Category =  entity.Category?.Translate() ?? "??",
            HazardLevel = entity.HazardLevel?.Translate() ?? "??",
            IsHazardous = entity.IsHazardous,
            ColorCode = entity.ColorCode?.Translate() ?? "??",
            DefaultStorage = entity.DefaultStorage,
            StandardConcentration = entity.StandardConcentration,
            MaterialFilePath = entity.MaterialFilePath
        };

    // Create Request → Entity
    public static ReagentType ToEntity(SaveReagentTypeRequest request)
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
            },
            Category = new LangStr
            {
                [Cultures.English] = request.CategoryEn ?? "",
                [Cultures.Estonian] = request.CategoryEt ?? ""
            },
            HazardLevel = new LangStr
            {
                [Cultures.English] = request.HazardLevelEn ?? "",
                [Cultures.Estonian] = request.DescriptionEt ?? ""
            },
            DefaultStorage =  request.DefaultStorage,
            IsHazardous = request.IsHazardous,
            ColorCode = new LangStr
            {
                [Cultures.English] = request.ColorCodeEn ?? "",
                [Cultures.Estonian] = request.ColorCodeEt ?? ""
            },
            StandardConcentration = request.StandardConcentration,
            MaterialFilePath = request.MaterialFilePath
            
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ReagentType entity, SaveReagentTypeRequest request)
    {
        entity.Name.SetTranslation(request.NameEn ?? "", Cultures.English);
        entity.Name.SetTranslation(request.NameEt ?? "", Cultures.Estonian);

        entity.Description ??= new LangStr();
        entity.Description.SetTranslation(request.DescriptionEn ?? "", Cultures.English);
        entity.Description.SetTranslation(request.DescriptionEt ?? "", Cultures.Estonian);

        entity.Category ??= new LangStr();
        entity.Category.SetTranslation(request.CategoryEn ?? "", Cultures.English);
        entity.Category.SetTranslation(request.CategoryEt ?? "", Cultures.Estonian);

        entity.HazardLevel ??= new LangStr();
        entity.HazardLevel.SetTranslation(request.HazardLevelEn ?? "", Cultures.English);
        entity.HazardLevel.SetTranslation(request.HazardLevelEt ?? "", Cultures.Estonian);

        entity.ColorCode ??= new LangStr();
        entity.ColorCode.SetTranslation(request.ColorCodeEn ?? "", Cultures.English);
        entity.ColorCode.SetTranslation(request.ColorCodeEt ?? "", Cultures.Estonian);

        entity.DefaultStorage = request.DefaultStorage;
        entity.IsHazardous = request.IsHazardous;
        entity.StandardConcentration = request.StandardConcentration;
        entity.MaterialFilePath = request.MaterialFilePath;
    }
}