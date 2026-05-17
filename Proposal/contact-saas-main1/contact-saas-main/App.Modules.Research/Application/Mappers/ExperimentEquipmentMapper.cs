using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;

namespace App.Modules.Project.Application.Mappers;

public static class ExperimentEquipmentMapper
{
    // Entity → Full Response
    public static ExperimentEquipmentResponse ToResponse(ExperimentEquipment entity)
        => new ()
        {
            Id = entity.Id,
            ExperimentName = entity.Experiment.ExperimentName,
            EquipementId = entity.EquipmentId,
        };

    // Create Request → Entity
    public static ExperimentEquipment ToEntity(SaveExperimentEquipmentRequest request)
        => new ()
        {
            ExperimentId = request.EquipementId,
            EquipmentId = request.EquipementId,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ExperimentEquipment entity, SaveExperimentEquipmentRequest request)
    {
        entity.ExperimentId = request.EquipementId;
        entity.EquipmentId = request.EquipementId;
    }
}