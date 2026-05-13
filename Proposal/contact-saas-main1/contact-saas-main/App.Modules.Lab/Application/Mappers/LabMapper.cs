using App.Shared.Domain;

namespace App.Modules.Lab.Application.Mapper;

public static class LabMapper
{
    // Entity → List Response
    public static LabListResponse ToListResponse(App.Domain.Entities.Lab entity)
        => new LabListResponse
        {
            Id = entity.Id,
            Name = entity.LabName.ToString()
        };

    // Entity → Full Response
    public static LabResponse ToResponse(App.Domain.Entities.Lab entity)
        => new LabResponse
        {
            Id = entity.Id,
            Name = entity.LabName.ToString(),
        };

    // Create Request → Entity
    public static App.Domain.Entities.Lab ToEntity(CreateLabRequest request)
        => new App.Domain.Entities.Lab
        {
            LabName = new LangStr { ["en"] = request.Name ?? "" },
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(App.Domain.Entities.Lab entity, UpdateLabRequest request)
    {
        entity.LabName = new LangStr { ["en"] = request.Name ?? "" };
    }
}