using System.Text.Json;
using App.Domain.Entities;

namespace App.Modules.Experiment.Application.Mapper;

public static class ExperimentTaskTypeMapper
{
    // Entity → List Response
    public static ExperimentTaskTypeListResponse ToListResponse(ExperimentTaskType entity)
        => new ExperimentTaskTypeListResponse
        {
            Id = entity.Id,
            Name = entity.GetName(),
            Description = entity.GetDescription()
        };

    // Entity → Full Response
    public static ExperimentTaskTypeResponse ToResponse(ExperimentTaskType entity)
        => new ExperimentTaskTypeResponse
        {
            Id = entity.Id,
            NameEn = entity.GetName("en"),
            NameEt = entity.GetName("et"),
            DescriptionEn = entity.GetDescription("en"),
            DescriptionEt = entity.GetDescription("et")
        };

    // Create Request → Entity
    public static ExperimentTaskType ToEntity(CreateExperimentTaskTypeRequest request)
        => new ExperimentTaskType
        {
            Id = request.Id,
            Name = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.NameEn ?? "", ["et"] = request.NameEt ?? "" }),
            Description = request.DescriptionEn == null && request.DescriptionEt == null ? null
                : JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.DescriptionEn ?? "", ["et"] = request.DescriptionEt ?? "" })
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ExperimentTaskType entity, UpdateExperimentTaskTypeRequest request)
    {
        entity.Id = request.Id;
        entity.Name = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.NameEn ?? "", ["et"] = request.NameEt ?? "" });
        if (request.DescriptionEn != null || request.DescriptionEt != null)
        {
            entity.Description = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.DescriptionEn ?? "", ["et"] = request.DescriptionEt ?? "" });
        }
    }
}