using App.Domain.Entities;

namespace App.Modules.Reagent.Application.Mappers;

public class ReagentTypeMapper
{
    // Entity → List Response
    public static ReagentTypeListResponse ToListResponse(ReagentType entity)
        => new ReagentTypeListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static ReagentTypeResponse ToResponse(ReagentType entity)
        => new ReagentTypeResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static ReagentType ToEntity(CreateReagentTypeRequest request)
        => new ReagentType
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ReagentType entity, UpdateReagentTypeRequest request)
    {
        entity.Id = request.Id;
    }
}