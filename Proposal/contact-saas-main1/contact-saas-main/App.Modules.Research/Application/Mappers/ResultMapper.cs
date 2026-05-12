using App.Domain.Entities;

namespace App.Modules.Project.Application.Mapper;

public static class ResultMapper
{
    // Entity → List Response
    public static ResultListResponse ToListResponse(Result entity)
        => new ResultListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static ResultResponse ToResponse(Result entity)
        => new ResultResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static Result ToEntity(CreateResultRequest request)
        => new Result
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Result entity, UpdateResultRequest request)
    {
        entity.Id = request.Id;
    }
}