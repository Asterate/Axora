// App.Modules.Equipment/Application/Mapper/EquipmentMapper.cs

using App.Modules.Equipment.Domain;
using App.Shared.Domain;

namespace App.Modules.Equipment.Application.Mapper;

public static class EquipmentCertificationTypeMapper
{
    // Entity → List Response
    public static EquipmentCertificationTypeListResponse ToListResponse(EquipmentCertificationType entity)
        => new EquipmentCertificationTypeListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static EquipmentCertificationTypeResponse ToResponse(EquipmentCertificationType entity)
        => new EquipmentCertificationTypeResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static EquipmentCertificationType ToEntity(CreateEquipmentCertificationTypeRequest request)
        => new EquipmentCertificationType
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(EquipmentCertificationType entity, UpdateEquipmentCertificationTypeRequest request)
    {
        entity.Id = request.Id;
    }
}