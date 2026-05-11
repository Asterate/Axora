using App.Domain.Entities;

namespace App.Modules.Experiment.Application.Mapper;

public static class ExperimentTaskTypeMapper
{
    // Entity → List Response
    public static ExperimentTaskTypeListResponse ToListResponse(ExperimentTaskType entity)
        => new ExperimentTaskTypeListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static ExperimentTaskTypeResponse ToResponse(ExperimentTaskType entity)
        => new ExperimentTaskTypeResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static ExperimentTaskType ToEntity(CreateExperimentTaskTypeRequest request)
        => new ExperimentTaskType
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ExperimentTaskType entity, UpdateExperimentTaskTypeRequest request)
    {
        entity.Id = request.Id;
    }
}