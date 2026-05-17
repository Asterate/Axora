using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Domain;

namespace App.Modules.Lab.Application.Mappers;

public static class InstituteLabMapper
{
    // Entity → Full Response
    public static InstituteLabResponse ToResponse(InstituteLab entity)
        => new ()
        {
            Id = entity.Id,
            InstituteId =  entity.InstituteId,
            LabName = entity.Lab.LabName.Translate() ?? "??"
        };

    // Create Request → Entity
    public static InstituteLab ToEntity(SaveInstituteLabRequest request)
        => new ()
        {
            InstituteId =  request.InstituteId,
            LabId =   request.LabId,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(InstituteLab entity, SaveInstituteLabRequest request)
    {
        entity.InstituteId = request.InstituteId;
        entity.LabId = request.LabId;
    }
    public static SaveInstituteLabRequest ToUpdateRequest(InstituteLab request)
    {
        return new SaveInstituteLabRequest
        {
            InstituteId =  request.InstituteId,
            LabId =   request.LabId,
        };
    }
}