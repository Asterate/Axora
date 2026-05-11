// App.Modules.Equipment/Application/Mapper/EquipmentMapper.cs

using App.Modules.Equipment.Domain;
using App.Shared.Domain;

namespace App.Modules.Equipment.Application.Mapper;

public static class CertificationTypeMapper
{
    // Entity → List Response
    public static CertificationTypeListResponse ToListResponse(CertificationType entity)
        => new CertificationTypeListResponse
        {
            Id = entity.Id,
            Name = entity.Name.ToString()
        };

    // Entity → Full Response
    public static CertificationTypeResponse ToResponse(CertificationType entity)
        => new CertificationTypeResponse
        {
            Id = entity.Id,
            Name = entity.Name.ToString(),
        };

    // Create Request → Entity
    public static CertificationType ToEntity(CreateCertificationTypeRequest request)
        => new CertificationType
        {
            Name = new LangStr { ["en"] = request.Name ?? "" },
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(CertificationType entity, UpdateCertificationTypeRequest request)
    {
        entity.Name = new LangStr { ["en"] = request.Name ?? "" };
    }
}