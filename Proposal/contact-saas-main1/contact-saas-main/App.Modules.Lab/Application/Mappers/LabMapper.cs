using App.Modules.Lab.Application.DTO;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Lab.Application.Mappers;

public static class LabMapper
{
    // Entity → Full Response
    public static LabResponse ToResponse(Domain.Lab entity)
        => new ()
        {
            Id = entity.Id,
            LabName = entity.LabName.Translate() ?? "??",
            LabAddress = entity.LabAddress,
            LabCapacity =  entity.LabCapacity,
            LabIsActive = entity.LabIsActive,
            LabTypeName = entity.LabType.Name.Translate() ?? "??",
        };

    // Create Request → Entity
    public static Domain.Lab ToEntity(SaveLabRequest request)
        => new ()
        {
            LabName = new LangStr
            {
                [Cultures.English] = request.LabNameEn ?? "",
                [Cultures.Estonian] = request.LabNameEt ?? "",
            },
            LabAddress =  request.LabAddress ?? "",
            LabCapacity =  request.LabCapacity,
            LabIsActive = request.LabIsActive,
            LabTypeId = request.LabTypeId,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Domain.Lab entity, SaveLabRequest request)
    {
        entity.LabName.SetTranslation(request.LabNameEn, Cultures.English);
        entity.LabName.SetTranslation(request.LabNameEt, Cultures.Estonian);
        entity.LabAddress = request.LabAddress;
        entity.LabCapacity = request.LabCapacity;
        entity.LabIsActive = request.LabIsActive;
        entity.LabTypeId = request.LabTypeId;
    }
}