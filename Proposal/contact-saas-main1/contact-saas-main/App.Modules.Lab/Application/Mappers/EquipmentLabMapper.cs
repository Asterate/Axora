using App.Domain.Entities;
using App.Modules.Equipment.Domain;

namespace App.Modules.Lab.Application.Mapper;

public static class EquipmentLabMapper
{
    // Entity → List Response
    public static EquipmentLabListResponse ToEquipmentLabResponse(EquipmentLab entity)
        => new EquipmentLabListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static EquipmentLabResponse ToResponse(EquipmentLab entity)
        => new EquipmentLabResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static EquipmentLab ToEntity(CreateEquipmentLabRequest request)
        => new EquipmentLab
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(EquipmentLab entity, UpdateEquipmentLabRequest request)
    {
        entity.Id = request.Id;
    }
}