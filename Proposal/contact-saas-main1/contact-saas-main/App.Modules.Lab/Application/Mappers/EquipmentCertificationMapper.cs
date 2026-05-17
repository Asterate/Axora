using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Domain;

namespace App.Modules.Lab.Application.Mappers;

public static class EquipmentCertificationMapper
{
    // Entity → List Response
    public static EquipmentCertificationListResponse ToListResponse(EquipmentCertification entity)
        => new ()
        {
            Id = entity.Id,
            EquipmentId = entity.EquipmentId,
            CertificationTypeId =  entity.CertificationTypeId
        };

    // Entity → Full Response
    public static EquipmentCertificationResponse ToResponse(EquipmentCertification entity)
        => new ()
        {
            Id = entity.Id,
            EquipmentId = entity.EquipmentId,
            CertificationTypeId = entity.CertificationTypeId,
            EquipmentName = entity.Equipment.EquipmentName.Translate() ?? "??",
            CertificationTypeName = entity.CertificationType.Name.Translate() ?? "??",
        };

    // Create Request → Entity
    public static EquipmentCertification ToEntity(CreateEquipmentCertificationRequest request)
        => new ()
        {
            EquipmentId = request.EquipmentId,
            CertificationTypeId =  request.CertificationTypeId
        };
}