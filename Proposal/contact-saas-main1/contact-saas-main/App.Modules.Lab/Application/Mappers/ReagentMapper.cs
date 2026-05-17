using App.Modules.Lab.Application.DTO;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Lab.Application.Mappers;

public class ReagentMapper
{
    // Entity → List Response
    public static ReagentListResponse ToListResponse(Lab.Domain.Reagent entity)
        => new ()
        {
            Id = entity.Id,
            ReagentName =  entity.ReagentName.Translate() ?? "??",
            ReagentDescription =  entity.ReagentDescription.Translate() ?? "??",
            CasNumber = entity.CasNumber,
            MolecularWeight =  entity.MolecularWeight,
            ReagentTypeId = entity.ReagentTypeId,
            ReagentTypeName = entity.ReagentType.Name.Translate() ?? "??"
        };

    // Entity → Full Response
    public static ReagentResponse ToResponse(Lab.Domain.Reagent entity)
        => new ()
        {
            Id = entity.Id,
            ReagentName =  entity.ReagentName.Translate() ?? "??",
            ReagentDescription =  entity.ReagentDescription.Translate() ?? "??",
            CasNumber = entity.CasNumber,
            MolecularWeight =  entity.MolecularWeight,
            ReagentTypeId = entity.ReagentTypeId,
            ChemicalFormula =  entity.ChemicalFormula,
            Concentration =   entity.Concentration,
            StorageConditions = entity.StorageConditions?.Translate() ?? "??",
            SafetyNotes = entity.SafetyNotes?.Translate() ?? "??",
            MaterialFilePath =  entity.MaterialFilePath
        };

    // Create Request → Entity
    public static Lab.Domain.Reagent ToEntity(SaveReagentRequest request)
        => new ()
        {
            ReagentName = new LangStr()
            {
                [Cultures.English] = request.ReagentNameEn,
                [Cultures.Estonian] = request.ReagentNameEt,
            },
            ReagentDescription =  new LangStr()
            {
                [Cultures.English] = request.ReagentDescriptionEn,
                [Cultures.Estonian] = request.ReagentDescriptionEt,
            },
            CasNumber = request.CasNumber,
            MolecularWeight =  request.MolecularWeight,
            ReagentTypeId = request.ReagentTypeId,
            ChemicalFormula =  request.ChemicalFormula,
            Concentration =   request.Concentration,
            StorageConditions = new LangStr()
            {
                [Cultures.English] = request.StorageConditionsEn ?? "??",
                [Cultures.Estonian] = request.StorageConditionsEt ?? "??",
            },
            SafetyNotes = new LangStr()
            {
                [Cultures.English] = request.SafetyNotesEn ?? "??",
                [Cultures.Estonian] = request.SafetyNotesEt ?? "??",
            },
            MaterialFilePath =  request.MaterialFilePath
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Lab.Domain.Reagent entity, SaveReagentRequest request)
    {
        
        entity.ReagentName.SetTranslation(request.ReagentNameEn ?? "", Cultures.English);
        entity.ReagentName.SetTranslation(request.ReagentNameEt ?? "", Cultures.Estonian);
        
        entity.ReagentDescription.SetTranslation(request.ReagentDescriptionEn ?? "", Cultures.English);
        entity.ReagentDescription.SetTranslation(request.ReagentDescriptionEt ?? "", Cultures.Estonian);

        entity.StorageConditions ??= new LangStr();
        entity.StorageConditions.SetTranslation(request.StorageConditionsEn ?? "", Cultures.English);
        entity.StorageConditions.SetTranslation(request.StorageConditionsEt ?? "", Cultures.Estonian);
        
        entity.SafetyNotes ??= new LangStr();
        entity.SafetyNotes.SetTranslation(request.SafetyNotesEn ?? "", Cultures.English);
        entity.SafetyNotes.SetTranslation(request.SafetyNotesEt ?? "", Cultures.Estonian);
        
        entity.CasNumber = request.CasNumber;
        entity.MolecularWeight = request.MolecularWeight;
        entity.ReagentTypeId = request.ReagentTypeId;
        entity.ChemicalFormula = request.ChemicalFormula;
        entity.Concentration = request.Concentration;
        entity.MaterialFilePath = request.MaterialFilePath;
    }
}