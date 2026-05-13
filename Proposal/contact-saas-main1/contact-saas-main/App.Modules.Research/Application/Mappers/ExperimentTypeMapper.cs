// App.Modules.Equipment/Application/Mapper/EquipmentMapper.cs

using System.Text.Json;
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
            Name = entity.GetName()
        };

    // Entity → Full Response
    public static ExperimentTypeResponse ToResponse(ExperimentType entity)
        => new ExperimentTypeResponse
        {
            Id = entity.Id,
            Name = entity.GetName(),
        };

    // Create Request → Entity
    public static ExperimentType ToEntity(CreateExperimentTypeRequest request)
        => new ExperimentType
        {
            Name = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.Name ?? "" }),
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ExperimentType entity, UpdateExperimentTypeRequest request)
    {
        entity.Name = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.Name ?? "" });
    }
}