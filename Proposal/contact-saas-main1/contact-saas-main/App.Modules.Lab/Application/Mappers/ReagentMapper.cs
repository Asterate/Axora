using App.Modules.Lab.Application.DTO;

namespace App.Modules.Lab.Application.Mappers;

public class ReagentMapper
{
    // Entity → List Response
    public static ReagentListResponse ToListResponse(Lab.Domain.Reagent entity)
        => new ()
        {
            Id = entity.Id,
            ReagentName =  entity.ReagentName,
            ReagentDescription =  entity.ReagentDescription,
            CasNumber = entity.CasNumber,
            MolecularWeight =  entity.MolecularWeight,
            ReagentTypeId = entity.ReagentTypeId,
            ReagentTypeName = entity.ReagentType.Name
        };

    // Entity → Full Response
    public static ReagentResponse ToResponse(Lab.Domain.Reagent entity)
        => new ()
        {
            Id = entity.Id,
            ReagentName =  entity.ReagentName,
            ReagentDescription =  entity.ReagentDescription,
            CasNumber = entity.CasNumber,
            MolecularWeight =  entity.MolecularWeight,
            ReagentTypeId = entity.ReagentTypeId,
            ChemicalFormula =  entity.ChemicalFormula,
            Concentration =   entity.Concentration,
            StorageConditions = entity.StorageConditions,
            SafetyNotes = entity.SafetyNotes,
            MaterialFilePath =  entity.MaterialFilePath
        };

    // Create Request → Entity
    public static Lab.Domain.Reagent ToEntity(CreateReagentRequest request)
        => new ()
        {
            ReagentName =  request.ReagentName,
            ReagentDescription =  request.ReagentDescription,
            CasNumber = request.CasNumber,
            MolecularWeight =  request.MolecularWeight,
            ReagentTypeId = request.ReagentTypeId,
            ChemicalFormula =  request.ChemicalFormula,
            Concentration =   request.Concentration,
            StorageConditions = request.StorageConditions,
            SafetyNotes = request.SafetyNotes,
            MaterialFilePath =  request.MaterialFilePath
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Lab.Domain.Reagent entity, UpdateReagentRequest request)
    {
        entity.Id = request.Id;
        entity.ReagentName = request.ReagentName;
        entity.ReagentDescription = request.ReagentDescription;
        entity.CasNumber = request.CasNumber;
        entity.MolecularWeight = request.MolecularWeight;
        entity.ReagentTypeId = request.ReagentTypeId;
        entity.ChemicalFormula = request.ChemicalFormula;
        entity.Concentration = request.Concentration;
        entity.StorageConditions = request.StorageConditions;
        entity.SafetyNotes = request.SafetyNotes;
        entity.MaterialFilePath = request.MaterialFilePath;
    }
}