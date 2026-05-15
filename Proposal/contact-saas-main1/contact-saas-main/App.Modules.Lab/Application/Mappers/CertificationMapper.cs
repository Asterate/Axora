using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Domain;
using App.Shared.Domain;

namespace App.Modules.Lab.Application.Mappers;

public static class CertificationMapper
{

    // Entity → Full Response
    public static CertificationResponse ToResponse(Certification entity)
        => new ()
        {
            Id = entity.Id,
            CertificationName = entity.CertificationName,
            HandedOver = entity.HandedOver,
            Expired = entity.Expired,
            InstituteUserId = entity.InstituteUserId,
            CertificationTypeId =  entity.CertificationTypeId
        };

    // Create Request → Entity
    public static Certification ToEntity(CreateCertificationRequest request)
        => new ()
        {
            CertificationName = new LangStr { ["en"] = request.CertificationName ?? "" },
            HandedOver = request.HandedOver,
            Expired = request.Expired,
            InstituteUserId = request.InstituteUserId,
            CertificationTypeId =  request.CertificationTypeId
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Certification entity, UpdateCertificationRequest request)
    {
        entity.CertificationName = new LangStr { ["en"] = request.CertificationName ?? "" };
        entity.HandedOver = request.HandedOver;
        entity.Expired = request.Expired;
        entity.InstituteUserId = request.InstituteUserId;
        entity.CertificationTypeId = request.CertificationTypeId;
    }
}