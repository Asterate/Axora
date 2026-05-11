using App.Domain.Entities;

namespace App.Modules.Experiment.Application.Mapper;

public static class ExperimentTaskMapper
{
    // Entity → List Response
    public static ExperimentTaskListResponse ToListResponse(ExperimentTask entity)
        => new ExperimentTaskListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static ExperimentTaskResponse ToResponse(ExperimentTask entity)
        => new ExperimentTaskResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static ExperimentTask ToEntity(CreateExperimentTaskRequest request)
        => new ExperimentTask
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ExperimentTask entity, UpdateExperimentTaskRequest request)
    {
        entity.Id = request.Id;
    }
}