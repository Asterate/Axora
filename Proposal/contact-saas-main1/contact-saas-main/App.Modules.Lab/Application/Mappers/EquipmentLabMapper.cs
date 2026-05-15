using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Domain;

namespace App.Modules.Lab.Application.Mappers;

public static class EquipmentLabMapper
{
    // Entity → Full Response
    public static EquipmentLabResponse ToResponse(EquipmentLab entity)
        => new ()
        {
            Id = entity.Id,
            Quantity =  entity.Quantity,
            LabId = entity.LabId,
            LabName = entity.Lab.LabName,
            EquipmentId =  entity.EquipmentId,
            EquipmentName = entity.Equipment.EquipmentName,
            CreatedAt = entity.CreatedAt,
            DeletedAt = entity.DeletedAt
        };

    // Create Request → Entity
    public static EquipmentLab ToEntity(CreateEquipmentLabRequest request)
        => new ()
        {
            Quantity =  request.Quantity,
            LabId = request.LabId,
            EquipmentId =  request.EquipmentId,
            CreatedAt = request.CreatedAt,
            DeletedAt = request.DeletedAt
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(EquipmentLab entity, UpdateEquipmentLabRequest request)
    {
        entity.Id = request.Id;
        entity.Quantity = request.Quantity;
        entity.LabId = request.LabId;
        entity.EquipmentId = request.EquipmentId;
        entity.CreatedAt = request.CreatedAt;
        entity.DeletedAt = request.DeletedAt;
    }
}