// App.Modules.Equipment/Application/Mapper/EquipmentMapper.cs

using System.Text.Json;
using App.Domain.Entities;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;
using App.Shared.Domain;

namespace App.Modules.Experiment.Application.Mapper;

public static class ExperimentTypeMapper
{
    // Entity → List Response
    public static ExperimentTypeListResponse ToListResponse(ExperimentType entity)
        => new ()
        {
            Id = entity.Id,
            Name = entity.GetName(),
            Description = entity.GetDescription()
        };

    // Entity → Full Response
    public static ExperimentTypeResponse ToResponse(ExperimentType entity)
        => new ()
        {
            Id = entity.Id,
            NameEn = entity.GetName("en"),
            NameEt = entity.GetName("et"),
            DescriptionEn = entity.GetDescription("en"),
            DescriptionEt = entity.GetDescription("et")
        };

    // Create Request → Entity
    public static ExperimentType ToEntity(CreateExperimentTypeRequest request)
        => new ()
        {
            Name = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.NameEn ?? "", ["et"] = request.NameEt ?? "" }),
            Description = request.DescriptionEn == null && request.DescriptionEt == null ? null
                : JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.DescriptionEn ?? "", ["et"] = request.DescriptionEt ?? "" })
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ExperimentType entity, UpdateExperimentTypeRequest request)
    {
        entity.Id = request.Id;
        entity.Name = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.NameEn ?? "", ["et"] = request.NameEt ?? "" });
        if (request.DescriptionEn != null || request.DescriptionEt != null)
        {
            entity.Description = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.DescriptionEn ?? "", ["et"] = request.DescriptionEt ?? "" });
        }
    }
    public static UpdateExperimentTypeRequest ToUpdateRequest(ExperimentType request)
    {
        return new UpdateExperimentTypeRequest
        {
            Id = request.Id,
            NameEn = request.GetName("en"),
            NameEt = request.GetName("et"),
            DescriptionEn = request.GetDescription("en"),
            DescriptionEt = request.GetDescription("et")
        };
    }
}