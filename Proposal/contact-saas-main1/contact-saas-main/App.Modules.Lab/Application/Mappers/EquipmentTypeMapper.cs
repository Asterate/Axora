// App.Modules.Equipment/Application/Mapper/EquipmentMapper.cs

using System.Text.Json;
using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Domain;

namespace App.Modules.Equipment.Application.Mapper;

public static class EquipmentTypeMapper
{
    // Entity → List Response
    public static EquipmentTypeListResponse ToListResponse(EquipmentType entity)
        => new EquipmentTypeListResponse
        {
            Id = entity.Id,
            Name = entity.GetName(),
            Description = entity.GetDescription()
        };

    // Entity → Full Response
    public static EquipmentTypeResponse ToResponse(EquipmentType entity)
        => new EquipmentTypeResponse
        {
            Id = entity.Id,
            NameEn = entity.GetName("en"),
            NameEt = entity.GetName("et"),
            DescriptionEn = entity.GetDescription("en"),
            DescriptionEt = entity.GetDescription("et")
        };

    // Create Request → Entity
    public static EquipmentType ToEntity(CreateEquipmentTypeRequest request)
        => new ()
        {
            Name = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.NameEn ?? "", ["et"] = request.NameEt ?? "" }),
            Description = request.DescriptionEn == null && request.DescriptionEt == null ? null
                : JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.DescriptionEn ?? "", ["et"] = request.DescriptionEt ?? "" })
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(EquipmentType entity, UpdateEquipmentTypeRequest request)
    {
        entity.Id = request.Id;
        entity.Name = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.NameEn ?? "", ["et"] = request.NameEt ?? "" });
        if (request.DescriptionEn != null || request.DescriptionEt != null)
        {
            entity.Description = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.DescriptionEn ?? "", ["et"] = request.DescriptionEt ?? "" });
        }
    }
}