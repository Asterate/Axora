// App.Modules.Equipment/Application/Mapper/EquipmentMapper.cs

using App.Domain.Entities;
using App.Shared.Domain;

namespace App.Modules.Experiment.Application.Mapper;

public static class ExperimentTypeMapper
{
    // Entity → List Response
    public static ExperimentTypeListResponse ToListResponse(ExperimentType entity)
        => new ExperimentTypeListResponse
        {
            Id = entity.Id,
            Name = entity.Name.ToString()
        };

    // Entity → Full Response
    public static ExperimentTypeResponse ToResponse(ExperimentType entity)
        => new ExperimentTypeResponse
        {
            Id = entity.Id,
            Name = entity.Name.ToString(),
        };

    // Create Request → Entity
    public static ExperimentType ToEntity(CreateExperimentTypeRequest request)
        => new ExperimentType
        {
            Name = new LangStr { ["en"] = request.Name ?? "" },
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ExperimentType entity, UpdateExperimentTypeRequest request)
    {
        entity.Name = new LangStr { ["en"] = request.Name ?? "" };
    }
}