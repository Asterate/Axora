using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Project.Application.Mappers;

public static class ResultMapper
{
    // Entity → List Response
    public static ResultListResponse ToListResponse(Result entity)
        => new ()
        {
            Id = entity.Id,
            ResultName =  entity.ResultName.Translate() ??  String.Empty,
            ExperimentName = entity.Experiment.ExperimentName.Translate(),
            ExperimentTaskName = entity.ExperimentTask.TaskName.Translate(),
            CreatedAt = entity.CreatedAt,
            ProjectName = entity.Project.ProjectName.Translate()
        };

    // Entity → Full Response
    public static ResultResponse ToResponse(Result entity)
        => new ()
        {
            Id = entity.Id,
            ResultName = entity.ResultName.Translate() ?? String.Empty,
            ExperimentName = entity.Experiment.ExperimentName.Translate(),
            ResultDescription  = entity.ResultDescription.Translate() ?? String.Empty,
            MeasurementName = entity.MeasurementName?.Translate() ?? String.Empty,
            MeasurementValue = entity.MeasurementValue,
            CreatedAt = entity.CreatedAt,
            Unit =  entity.Unit?.Translate(),
            Notes = entity.Notes?.Translate(),
            FilePath = entity.FilePath,
            ExperimentTaskName = entity.ExperimentTask.TaskName.Translate(),
            ProjectName = entity.Project.ProjectName.Translate()
        };

    // Create Request → Entity
    public static Result ToEntity(SaveResultRequest request)
        => new ()
        {
            ResultName = new LangStr()
            {
                [Cultures.English] =  request.ResultNameEn,
                [Cultures.Estonian] =  request.ResultNameEt,
            },
            ExperimentId = request.ExperimentId,
            ResultDescription = new LangStr()
            {
            [Cultures.English] =  request.ResultDescriptionEn,
            [Cultures.Estonian] =  request.ResultDescriptionEt,
            },
            MeasurementName = new LangStr()
            {
                [Cultures.English] =  request.MeasurementNameEn ?? String.Empty,
                [Cultures.Estonian] =  request.MeasurementNameEt ??  String.Empty,
            },
            MeasurementValue = request.MeasurementValue,
            Unit = new LangStr()
            {
                [Cultures.English] =  request.UnitEn ?? String.Empty,
                [Cultures.Estonian] =  request.UnitEt ??  String.Empty,
            },
            Notes = new LangStr()
            {
                [Cultures.English] =  request.NotesEn ?? String.Empty,
                [Cultures.Estonian] =  request.NotesEt ??  String.Empty,
            },
            FilePath = request.FilePath,
            ExperimentTaskId = request.ExperimentId,
            ProjectId = request.ProjectId
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Result entity, SaveResultRequest request)
    {
        entity.ResultName.SetTranslation(request.ResultNameEn, Cultures.English);
        entity.ResultName.SetTranslation(request.ResultNameEt, Cultures.Estonian);
        
        entity.ResultDescription.SetTranslation(request.ResultDescriptionEt, Cultures.Estonian);
        entity.ResultDescription.SetTranslation(request.ResultDescriptionEn, Cultures.English);

        entity.MeasurementName ??= new LangStr();
        entity.MeasurementName.SetTranslation(request.MeasurementNameEn ?? String.Empty,Cultures.English );
        entity.MeasurementName.SetTranslation(request.MeasurementNameEt ?? String.Empty,Cultures.Estonian);
        
        entity.Unit ??= new LangStr();
        entity.Unit.SetTranslation(request.UnitEn ?? String.Empty,Cultures.English);
        entity.Unit.SetTranslation(request.UnitEt ?? String.Empty,Cultures.Estonian);
        
        entity.Notes ??= new LangStr();
        entity.Notes.SetTranslation(request.NotesEn ?? String.Empty,Cultures.English);
        entity.Notes.SetTranslation(request.NotesEt ?? String.Empty, Cultures.Estonian);
        
        entity.MeasurementValue = request.MeasurementValue;
        entity.ExperimentId = request.ExperimentId;
        entity.ExperimentTaskId = request.ExperimentId;
        entity.ProjectId = request.ProjectId;
    }
    public static SaveResultRequest ToUpdateRequest(Result request)
    {
        return new SaveResultRequest
        {
            ResultNameEn = request.ResultName.Translate("en") ?? String.Empty,
            ResultNameEt = request.ResultName.Translate("et") ?? String.Empty,
            ExperimentId = request.ExperimentId,
            ResultDescriptionEn  = request.ResultDescription.Translate("en") ?? String.Empty,
            ResultDescriptionEt  = request.ResultDescription.Translate("et") ?? String.Empty,
            MeasurementNameEn = request.MeasurementName?.Translate("en") ?? String.Empty,
            MeasurementNameEt = request.MeasurementName?.Translate("et") ?? String.Empty,
            MeasurementValue = request.MeasurementValue,
            UnitEn =  request.Unit?.Translate("en") ?? String.Empty,
            UnitEt =  request.Unit?.Translate("et") ?? String.Empty,
            NotesEn = request.Notes?.Translate("en") ?? String.Empty,
            NotesEt = request.Notes?.Translate("et") ?? String.Empty,
            FilePath = request.FilePath,
            ProjectId = request.ProjectId
            
        };
    }
}