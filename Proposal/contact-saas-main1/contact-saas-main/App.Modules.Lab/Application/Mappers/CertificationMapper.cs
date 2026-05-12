// App.Modules.Equipment/Application/Mapper/EquipmentMapper.cs

using App.Modules.Equipment.Domain;
using App.Shared.Domain;

namespace App.Modules.Equipment.Application.Mapper;

public static class CertificationMapper
{
    // Entity → List Response
    public static CertificationListResponse ToListResponse(Certification entity)
        => new CertificationListResponse
        {
            Id = entity.Id,
            Name = entity.CertificationName.ToString()
        };

    // Entity → Full Response
    public static CertificationResponse ToResponse(Certification entity)
        => new CertificationResponse
        {
            Id = entity.Id,
            Name = entity.CertificationName.ToString(),
        };

    // Create Request → Entity
    public static Certification ToEntity(CreateCertificationRequest request)
        => new Certification
        {
            CertificationName = new LangStr { ["en"] = request.Name ?? "" },
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Certification entity, UpdateCertificationRequest request)
    {
        entity.CertificationName = new LangStr { ["en"] = request.Name ?? "" };
    }
}