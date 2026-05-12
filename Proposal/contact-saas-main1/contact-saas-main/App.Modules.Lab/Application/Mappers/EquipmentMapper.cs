// App.Modules.Equipment/Application/Mapper/EquipmentMapper.cs

using App.Shared.Domain;

namespace App.Modules.Equipment.Application.Mapper;

public static class EquipmentMapper
{
    // Entity → List Response
    public static EquipmentListResponse ToListResponse(Domain.Equipment entity)
        => new EquipmentListResponse
        {
            Id = entity.Id,
            Name = entity.EquipmentName.ToString()
        };

    // Entity → Full Response
    public static EquipmentResponse ToResponse(Domain.Equipment entity)
        => new EquipmentResponse
        {
            Id = entity.Id,
            Name = entity.EquipmentName.ToString(),
        };

    // Create Request → Entity
    public static Domain.Equipment ToEntity(CreateEquipmentRequest request)
        => new Domain.Equipment
        {
            EquipmentName = new LangStr { ["en"] = request.Name ?? "" },
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Domain.Equipment entity, UpdateEquipmentRequest request)
    {
        entity.EquipmentName = new LangStr { ["en"] = request.Name ?? "" };
    }
}