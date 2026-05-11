// App.Modules.Equipment/Application/Mapper/EquipmentMapper.cs

using App.Modules.Equipment.Domain;
using App.Shared.Domain;

namespace App.Modules.Equipment.Application.Mapper;

public static class EquipmentTypeMapper
{
    // Entity → List Response
    public static EquipmentTypeListResponse ToListResponse(EquipmentType entity)
        => new EquipmentTypeListResponse
        {
            Id = entity.Id,
            Name = entity.Name.ToString()
        };

    // Entity → Full Response
    public static EquipmentTypeResponse ToResponse(EquipmentType entity)
        => new EquipmentTypeResponse
        {
            Id = entity.Id,
            Name = entity.Name.ToString(),
        };

    // Create Request → Entity
    public static EquipmentType ToEntity(CreateEquipmentTypeRequest request)
        => new EquipmentType
        {
            Name = new LangStr { ["en"] = request.Name ?? "" },
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(EquipmentType entity, UpdateEquipmentTypeRequest request)
    {
        entity.Name = new LangStr { ["en"] = request.Name ?? "" };
    }
}