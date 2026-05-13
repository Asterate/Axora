using System.Text.Json;
using App.Domain.Entities;
using App.Shared.Domain;

namespace App.Modules.Institute.Application.Mapper;

public static class InstituteTypeMapper
{
    // Entity → List Response
    public static InstituteTypeListResponse ToListResponse(InstituteType entity)
        => new InstituteTypeListResponse
        {
            Id = entity.Id,
            Name = entity.GetName()
        };

    // Entity → Full Response
    public static InstituteTypeResponse ToResponse(InstituteType entity)
        => new InstituteTypeResponse
        {
            Id = entity.Id,
            Name = entity.GetName(),
        };

    // Create Request → Entity
    public static InstituteType ToEntity(CreateInstituteTypeRequest request)
        => new InstituteType
        {
            Name = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.Name ?? "" }),
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(InstituteType entity, UpdateInstituteTypeRequest request)
    {
        entity.Name = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.Name ?? "" });
    }
}