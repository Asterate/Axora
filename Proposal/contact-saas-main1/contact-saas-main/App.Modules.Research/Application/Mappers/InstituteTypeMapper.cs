using System.Text.Json;
using App.Domain.Entities;
using App.Shared.Domain;

namespace App.Modules.Institute.Application.Mapper;

public static class InstituteTypeMapper
{
    // Entity → List Response
    public static InstituteTypeListResponse ToListResponse(InstituteType entity)
        => new ()
        {
            Id = entity.Id,
            Name = entity.GetName(),
            Description = entity.GetDescription()
        };

    // Entity → Full Response
    public static InstituteTypeResponse ToResponse(InstituteType entity)
        => new ()
        {
            Id = entity.Id,
            NameEn = entity.GetName("en"),
            NameEt = entity.GetName("et"),
            DescriptionEn = entity.GetDescription("en"),
            DescriptionEt = entity.GetDescription("et")
        };

    // Create Request → Entity
    public static InstituteType ToEntity(CreateInstituteTypeRequest request)
        => new InstituteType
        {
            Id = request.Id,
            Name = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.NameEn ?? "", ["et"] = request.NameEt ?? "" }),
            Description = request.DescriptionEn == null && request.DescriptionEt == null ? null
                : JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.DescriptionEn ?? "", ["et"] = request.DescriptionEt ?? "" })
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(InstituteType entity, UpdateInstituteTypeRequest request)
    {
        entity.Id = request.Id;
        entity.Name = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.NameEn ?? "", ["et"] = request.NameEt ?? "" });
        if (request.DescriptionEn != null || request.DescriptionEt != null)
        {
            entity.Description = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.DescriptionEn ?? "", ["et"] = request.DescriptionEt ?? "" });
        }
    }
}