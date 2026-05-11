using App.Domain.Entities;
using App.Modules.Equipment.Domain;

namespace App.Modules.Lab.Application.Mapper;

public static class ReagentLabMapper
{
    // Entity → List Response
    public static ReagentLabListResponse ToReagentLabResponse(ReagentLab entity)
        => new ReagentLabListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static ReagentLabResponse ToResponse(ReagentLab entity)
        => new ReagentLabResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static ReagentLab ToEntity(CreateReagentLabRequest request)
        => new ReagentLab
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ReagentLab entity, UpdateReagentLabRequest request)
    {
        entity.Id = request.Id;
    }
}