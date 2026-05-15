// App.Modules.Equipment/Application/Mapper/EquipmentMapper.cs

using App.Modules.Lab.Application.DTO;
using App.Shared.Domain;

namespace App.Modules.Lab.Application.Mappers;

public static class EquipmentMapper
{
    // Entity → List Response
    public static EquipmentListResponse ToListResponse(Lab.Domain.Equipment entity)
        => new ()
        {
            Id = entity.Id,
            EquipmentName = entity.EquipmentName,
            EquipmentSerialCode = entity.EquipmentSerialCode,
            EquipmentTypeId = entity.EquipmentTypeId,
        };

    // Entity → Full Response
    public static EquipmentResponse ToResponse(Lab.Domain.Equipment entity)
        => new ()
        {
            Id = entity.Id,
            EquipmentName = entity.EquipmentName,
            EquipmentSerialCode = entity.EquipmentSerialCode,
            EquipmentTypeId = entity.EquipmentTypeId,
            ManualFilePath =  entity.ManualFilePath,
            EquipmentCertificationTypes = entity.EquipmentCertificationTypes,
        };

    // Create Request → Entity
    public static Lab.Domain.Equipment ToEntity(CreateEquipmentRequest request)
        => new()
        {
            EquipmentName = new LangStr { ["en"] = request.EquipmentName ?? "" },
            EquipmentSerialCode = request.EquipmentSerialCode,
            EquipmentTypeId = request.EquipmentTypeId,
            ManualFilePath =  request.ManualFilePath,
            EquipmentCertificationTypes = request.EquipmentCertificationTypes,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Lab.Domain.Equipment entity, UpdateEquipmentRequest request)
    {
        entity.Id = request.Id;
        entity.EquipmentName = new LangStr { ["en"] = request.EquipmentName ?? "" };
        entity.EquipmentSerialCode = request.EquipmentSerialCode;
        entity.EquipmentTypeId = request.EquipmentTypeId;
        entity.ManualFilePath = request.ManualFilePath;
        entity.EquipmentCertificationTypes = request.EquipmentCertificationTypes;
    }
}