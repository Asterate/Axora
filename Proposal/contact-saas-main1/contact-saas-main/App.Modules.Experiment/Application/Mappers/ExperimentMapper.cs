// App.Modules.Equipment/Application/Mapper/EquipmentMapper.cs

using App.Shared.Domain;

namespace App.Modules.Experiment.Application.Mapper;

public static class ExperimentMapper
{
    // Entity → List Response
    public static ExperimentListResponse ToListResponse(Domain.Entities.Experiment entity)
        => new ExperimentListResponse
        {
            Id = entity.Id,
            Name = entity.ExperimentName.ToString()
        };

    // Entity → Full Response
    public static ExperimentResponse ToResponse(Domain.Entities.Experiment entity)
        => new ExperimentResponse
        {
            Id = entity.Id,
            Name = entity.ExperimentName.ToString(),
        };

    // Create Request → Entity
    public static Domain.Entities.Experiment ToEntity(CreateExperimentRequest request)
        => new Domain.Entities.Experiment
        {
            ExperimentName = new LangStr { ["en"] = request.ExperimentName ?? "" },
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Domain.Entities.Experiment entity, UpdateExperimentRequest request)
    {
        entity.ExperimentName = new LangStr { ["en"] = request.ExperimentName ?? "" };
    }
}