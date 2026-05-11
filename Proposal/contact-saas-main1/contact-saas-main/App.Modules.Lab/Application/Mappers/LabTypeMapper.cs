using App.Domain.Entities;
using App.Shared.Domain;

namespace App.Modules.Lab.Application.Mapper;

public static class LabTypeMapper
{
    // Entity → List Response
    public static LabTypeListResponse ToListTypeResponse(LabType entity)
        => new LabTypeListResponse
        {
            Id = entity.Id,
            Name = entity.Name.ToString()
        };

    // Entity → Full Response
    public static LabTypeResponse ToResponse(LabType entity)
        => new LabTypeResponse
        {
            Id = entity.Id,
            Name = entity.Name.ToString(),
        };

    // Create Request → Entity
    public static LabType ToEntity(CreateLabTypeRequest request)
        => new LabType
        {
            Name = new LangStr { ["en"] = request.Name ?? "" },
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(LabType entity, UpdateLabTypeRequest request)
    {
        entity.Name = new LangStr { ["en"] = request.Name ?? "" };
    }
}