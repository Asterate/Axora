using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Domain;

namespace App.Modules.Lab.Application.Mappers;

public static class ReagentLabMapper
{
    public static ReagentLabResponse ToReagentLabResponse(ReagentLab entity)
        => new ()
        {
            Id = entity.Id,
            Quantity =  entity.Quantity,
            Unit = entity.Unit,
            LabId = entity.LabId,
            LabName = entity.Lab.LabName.Translate(),
            ReagentId = entity.ReagentId,
            ReagentName = entity.Reagent.ReagentName.Translate(),
        };

    // Create Request → Entity
    public static ReagentLab ToEntity(SaveReagentLabRequest request)
        => new ()
        {
            Quantity =  request.Quantity,
            Unit = request.Unit,
            LabId = request.LabId,
            ReagentId = request.ReagentId,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ReagentLab entity, SaveReagentLabRequest request)
    {
        entity.Quantity = request.Quantity;
        entity.Unit = request.Unit;
        entity.LabId = request.LabId;
        entity.ReagentId = request.ReagentId;
    }
}