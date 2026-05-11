// App.Modules.Equipment/Application/Mapper/EquipmentMapper.cs

using App.Domain.Entities;
using App.Shared.Domain;

namespace App.Modules.Experiment.Application.Mapper;

public static class ExperimentEquipmentMapper
{
    // Entity → List Response
    public static ExperimentEquipmentListResponse ToListResponse(ExperimentEquipment entity)
        => new ExperimentEquipmentListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static ExperimentEquipmentResponse ToResponse(ExperimentEquipment entity)
        => new ExperimentEquipmentResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static ExperimentEquipment ToEntity(CreateExperimentEquipmentRequest request)
        => new ExperimentEquipment
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ExperimentEquipment entity, UpdateExperimentEquipmentRequest request)
    {
        request.Id = entity.Id;
    }
}