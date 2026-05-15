using System.Text.Json;
using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Domain;

namespace App.Modules.Lab.Application.Mappers;

public class ReagentTypeMapper
{
    // Entity → List Response
    public static ReagentTypeListResponse ToListResponse(ReagentType entity)
        => new ()
        {
            Id = entity.Id,
            Name = entity.GetName() ?? String.Empty,
            Description = entity.GetDescription(),
            Category =  entity.GetCategory(),
            HazardLevel = entity.GetHazardLevel(),
            IsHazardous = entity.IsHazardous,
            ColorCode = entity.GetColorCode()
        };

    // Entity → Full Response
    public static ReagentTypeResponse ToResponse(ReagentType entity)
        => new ()
        {
            Id = entity.Id,
            NameEn = entity.GetName("en") ?? String.Empty,
            NameEt = entity.GetName("et") ?? String.Empty,
            DescriptionEn = entity.GetDescription("en"),
            DescriptionEt = entity.GetDescription("et"),
            Category =  entity.GetCategory("en"),
            HazardLevel = entity.GetHazardLevel("en"),
            IsHazardous = entity.IsHazardous,
            ColorCode = entity.GetColorCode("en"),
            DefaultStorage = entity.DefaultStorage,
            StandardConcentration = entity.StandardConcentration,
            MaterialFilePath = entity.MaterialFilePath
        };

    // Create Request → Entity
    public static ReagentType ToEntity(CreateReagentTypeRequest request)
        => new ()
        {
            Name = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.NameEn ?? "", ["et"] = request.NameEt ?? "" }),
            Description = request.DescriptionEn == null && request.DescriptionEt == null ? null
                : JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.DescriptionEn ?? "", ["et"] = request.DescriptionEt ?? "" }),
            Category = request.Category,
            HazardLevel = request.HazardLevel,
            DefaultStorage =  request.DefaultStorage,
            IsHazardous = request.IsHazardous,
            ColorCode = request.ColorCode,
            StandardConcentration = request.StandardConcentration,
            MaterialFilePath = request.MaterialFilePath
            
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ReagentType entity, UpdateReagentTypeRequest request)
    {
        entity.Id = request.Id;
        entity.Name = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.NameEn ?? "", ["et"] = request.NameEt ?? "" });
        if (request.DescriptionEn != null || request.DescriptionEt != null)
        {
            entity.Description = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.DescriptionEn ?? "", ["et"] = request.DescriptionEt ?? "" });
        }

        entity.Category = request.Category;
        entity.HazardLevel = request.HazardLevel;
        entity.DefaultStorage = request.DefaultStorage;
        entity.IsHazardous = request.IsHazardous;
        entity.ColorCode = request.ColorCode;
        entity.StandardConcentration = request.StandardConcentration;
        entity.MaterialFilePath = request.MaterialFilePath;
    }
}