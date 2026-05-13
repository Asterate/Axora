using System.Text.Json;
using App.Shared.Domain;

namespace App.Modules.Institute.Application.Mapper;

public static class InstituteMapper
{
    // Entity → List Response
    public static InstituteListResponse ToListResponse(Domain.Entities.Institute entity)
        => new InstituteListResponse
        {
            Id = entity.Id,
            Name = entity.GetInstituteName()
        };

    // Entity → Full Response
    public static InstituteResponse ToResponse(Domain.Entities.Institute entity)
        => new InstituteResponse
        {
            Id = entity.Id,
            Name = entity.GetInstituteName(),
        };

    // Create Request → Entity
    public static Domain.Entities.Institute ToEntity(CreateInstituteRequest request)
        => new Domain.Entities.Institute
        {
            InstituteName = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.InstituteName?.ToString() ?? "" }),
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Domain.Entities.Institute entity, UpdateInstituteRequest request)
    {
        entity.InstituteName = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.InstituteName?.ToString() ?? "" });
    }
}