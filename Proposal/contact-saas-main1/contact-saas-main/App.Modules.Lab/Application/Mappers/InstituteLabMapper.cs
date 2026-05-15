using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Domain;
using App.Shared.Domain;

namespace App.Modules.Lab.Application.Mappers;

public static class InstituteLabMapper
{
    // Entity → Full Response
    public static InstituteLabResponse ToResponse(InstituteLab entity)
        => new ()
        {
            Id = entity.Id,
            InstituteId =  entity.InstituteId,
            LabName = entity.Lab.LabName
        };

    // Create Request → Entity
    public static InstituteLab ToEntity(CreateInstituteLabRequest request)
        => new ()
        {
            InstituteId =  request.InstituteId,
            LabId =   request.LabId,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(InstituteLab entity, UpdateInstituteLabRequest request)
    {
        entity.Id = request.Id;
        entity.InstituteId = request.InstituteId;
        entity.LabId = request.LabId;
    }
    public static UpdateInstituteLabRequest ToUpdateRequest(InstituteLab request)
    {
        return new UpdateInstituteLabRequest
        {
            Id = request.Id,
            InstituteId =  request.InstituteId,
            LabId =   request.LabId,
        };
    }
}