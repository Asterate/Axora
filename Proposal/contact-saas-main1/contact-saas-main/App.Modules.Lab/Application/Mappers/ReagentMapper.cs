namespace App.Modules.Reagent.Application.Mappers;

public class ReagentMapper
{
    // Entity → List Response
    public static ReagentListResponse ToListResponse(Domain.Entities.Reagent entity)
        => new ReagentListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static ReagentResponse ToResponse(Domain.Entities.Reagent entity)
        => new ReagentResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static Domain.Entities.Reagent ToEntity(CreateReagentRequest request)
        => new Domain.Entities.Reagent
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Domain.Entities.Reagent entity, UpdateReagentRequest request)
    {
        entity.Id = request.Id;
    }
}