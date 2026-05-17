// App.Modules.Equipment/Application/Mapper/EquipmentMapper.cs

using App.Modules.Lab.Application.DTO;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Lab.Application.Mappers;

public static class EquipmentMapper
{
    // Entity → List Response
    public static EquipmentListResponse ToListResponse(Lab.Domain.Equipment entity)
        => new ()
        {
            Id = entity.Id,
            EquipmentName = entity.EquipmentName.Translate() ?? "??",
            EquipmentSerialCode = entity.EquipmentSerialCode,
            EquipmentTypeId = entity.EquipmentTypeId,
        };

    // Entity → Full Response
    public static EquipmentResponse ToResponse(Lab.Domain.Equipment entity)
        => new ()
        {
            Id = entity.Id,
            EquipmentName = entity.EquipmentName.Translate() ?? "??",
            EquipmentSerialCode = entity.EquipmentSerialCode,
            EquipmentTypeId = entity.EquipmentTypeId,
            ManualFilePath =  entity.ManualFilePath,
            EquipmentCertificationTypes = entity.EquipmentCertificationTypes,
        };

    // Create Request → Entity
    public static Lab.Domain.Equipment ToEntity(SaveEquipmentRequest request)
        => new()
        {
            EquipmentName = new LangStr { [Cultures.English] = request.EquipmentNameEn ?? "??", 
                [Cultures.Estonian] =  request.EquipmentNameEt ?? "??", },
            EquipmentSerialCode = request.EquipmentSerialCode,
            EquipmentTypeId = request.EquipmentTypeId,
            ManualFilePath =  request.ManualFilePath,
            EquipmentCertificationTypes = request.EquipmentCertificationTypes,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Lab.Domain.Equipment entity, SaveEquipmentRequest request)
    {
        entity.EquipmentName.SetTranslation( request.EquipmentNameEn ?? "", Cultures.English );
        entity.EquipmentName.SetTranslation( request.EquipmentNameEt ?? "", Cultures.Estonian );
        entity.EquipmentSerialCode = request.EquipmentSerialCode;
        entity.EquipmentTypeId = request.EquipmentTypeId;
        entity.ManualFilePath = request.ManualFilePath;
        entity.EquipmentCertificationTypes = request.EquipmentCertificationTypes;
    }
}