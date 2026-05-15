using App.Modules.Lab.Application.DTO;
using App.Shared.Domain;

namespace App.Modules.Lab.Application.Mappers;

public static class LabMapper
{
    // Entity → Full Response
    public static LabResponse ToResponse(Domain.Lab entity)
        => new ()
        {
            Id = entity.Id,
            LabName = entity.LabName,
            LabAddress = entity.LabAddress,
            LabCapacity =  entity.LabCapacity,
            LabIsActive = entity.LabIsActive,
            LabTypeName = entity.LabType.Name
        };

    // Create Request → Entity
    public static Domain.Lab ToEntity(CreateLabRequest request)
        => new ()
        {
            LabName = new LangStr { ["en"] = request.LabName ?? "" },
            LabAddress =  new LangStr { ["en"] = request.LabAddress ?? "" },
            LabCapacity =  request.LabCapacity,
            LabIsActive = request.LabIsActive,
            LabTypeId = request.LabTypeId,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Domain.Lab entity, UpdateLabRequest request)
    {
        entity.Id = request.Id;
        entity.LabName = new LangStr { ["en"] = request.LabName ?? "" };
        entity.LabAddress = new LangStr { ["en"] = request.LabAddress ?? "" };
        entity.LabCapacity = request.LabCapacity;
        entity.LabIsActive = request.LabIsActive;
        entity.LabTypeId = request.LabTypeId;
    }
}