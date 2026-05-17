using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Domain;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Lab.Application.Mappers;

public static class CertificationMapper
{

    // Entity → Full Response
    public static CertificationResponse ToResponse(Certification entity)
        => new ()
        {
            Id = entity.Id,
            CertificationName = entity.CertificationName.Translate() ?? "??",
            HandedOver = entity.HandedOver,
            Expired = entity.Expired,
            InstituteUserId = entity.InstituteUserId,
            CertificationTypeId =  entity.CertificationTypeId
        };

    // Create Request → Entity
    public static Certification ToEntity(SaveCertificationRequest request)
        => new ()
        {
            CertificationName = new LangStr
            {
                [Cultures.English] = request.CertificationNameEn ?? "",
                [Cultures.Estonian] = request.CertificationNameEt ?? ""
            },
            HandedOver = request.HandedOver,
            Expired = request.Expired,
            InstituteUserId = request.InstituteUserId,
            CertificationTypeId =  request.CertificationTypeId
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Certification entity, SaveCertificationRequest request)
    {
        entity.CertificationName.SetTranslation(request.CertificationNameEn ?? "", Cultures.English);
        entity.CertificationName.SetTranslation(request.CertificationNameEt ?? "", Cultures.Estonian);
        entity.HandedOver = request.HandedOver;
        entity.Expired = request.Expired;
        entity.InstituteUserId = request.InstituteUserId;
        entity.CertificationTypeId = request.CertificationTypeId;
    }
}