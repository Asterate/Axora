using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;
using App.Shared.Domain;

namespace App.Modules.Project.Application.Mappers;

public static class ResultMapper
{
    // Entity → List Response
    public static ResultListResponse ToListResponse(Result entity)
        => new ()
        {
            Id = entity.Id,
            ResultName =  entity.ResultName,
            ExperimentName = entity.Experiment.ExperimentName,
            ExperimentTaskName = entity.ExperimentTask.TaskName,
            CreatedAt = entity.CreatedAt,
            ProjectName = entity.Project.ProjectName
        };

    // Entity → Full Response
    public static ResultResponse ToResponse(Result entity)
        => new ()
        {
            Id = entity.Id,
            ResultName = entity.ResultName,
            ExperimentName = entity.Experiment.ExperimentName,
            ResultDescription  = entity.ResultDescription,
            MeasurementName = entity.MeasurementName,
            MeasurementValue = entity.MeasurementValue,
            CreatedAt = entity.CreatedAt,
            Unit =  entity.Unit,
            Notes = entity.Notes,
            FilePath = entity.FilePath,
            ExperimentTaskName = entity.ExperimentTask.TaskName,
            ProjectName = entity.Project.ProjectName
        };

    // Create Request → Entity
    public static Result ToEntity(CreateResultRequest request)
        => new ()
        {
            ResultName = request.ResultName,
            ExperimentId = request.ExperimentId,
            ResultDescription  = request.ResultDescription,
            MeasurementName = request.MeasurementName,
            MeasurementValue = request.MeasurementValue,
            CreatedAt = request.CreatedAt,
            Unit =  request.Unit,
            Notes = request.Notes,
            FilePath = request.FilePath,
            ExperimentTaskId = request.ExperimentId,
            ProjectId = request.ProjectId
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Result entity, UpdateResultRequest request)
    {
        entity.Id = request.Id;
        entity.ResultName = request.ResultName;
        entity.ExperimentId = request.ExperimentId;
        entity.ResultDescription = request.ResultDescription;
        entity.MeasurementName = request.MeasurementName;
        entity.MeasurementValue = request.MeasurementValue;
        entity.CreatedAt = request.CreatedAt;
        entity.Unit = request.Unit;
        entity.Notes = request.Notes;
        entity.ExperimentTaskId = request.ExperimentId;
        entity.ProjectId = request.ProjectId;
    }
    public static UpdateResultRequest ToUpdateRequest(Result request)
    {
        return new UpdateResultRequest
        {
            Id = request.Id,
            ResultName = request.ResultName,
            ExperimentId = request.ExperimentId,
            ResultDescription  = request.ResultDescription,
            MeasurementName = request.MeasurementName,
            MeasurementValue = request.MeasurementValue,
            CreatedAt = request.CreatedAt,
            Unit =  request.Unit,
            Notes = request.Notes,
            FilePath = request.FilePath,
            ProjectId = request.ProjectId
            
        };
    }
}