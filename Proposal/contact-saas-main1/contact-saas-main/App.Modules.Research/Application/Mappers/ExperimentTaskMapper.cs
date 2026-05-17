using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Project.Application.Mappers;

public static class ExperimentTaskMapper
{
    // Entity → List Response
    public static ExperimentTaskListResponse ToListResponse(ExperimentTask entity)
        => new ()
        {
            Id = entity.Id,
            Name = entity.TaskName.Translate() ?? String.Empty,
            Description = entity.TaskDescription?.Translate() ?? String.Empty,
            Priority = entity.Priority,
            ExperimentName = entity.Experiment.ExperimentName.Translate() ?? String.Empty,
            Status = entity.Status,
            CreatedAt =  entity.CreatedAt,
            UpdatedAt =  entity.UpdatedAt,
        };

    // Entity → Full Response
    public static ExperimentTaskResponse ToResponse(ExperimentTask entity)
        => new ()
        {
            Id = entity.Id,
            TaskName = entity.TaskName.Translate() ?? String.Empty,
            Description = entity.TaskDescription ?? String.Empty,
            Priority = entity.Priority,
            ExperimentId = entity.ExperimentId,
            Status = entity.Status,
            CreatedAt =  entity.CreatedAt,
            UpdatedAt =  entity.UpdatedAt,
            TaskTypeId =  entity.TaskTypeId,
            AssignedUserId = entity.AssignedUserId,
        };

    // Create Request → Entity
    public static ExperimentTask ToEntity(SaveExperimentTaskRequest request)
        => new ()
        {
            TaskName = new LangStr()
            {
                [Cultures.English] =  request.TaskNameEn,
                [Cultures.Estonian] =   request.TaskNameEt,
            },
            TaskDescription = new LangStr()
            {
                [Cultures.English] =  request.TaskDescriptionEn ??  String.Empty,
                [Cultures.Estonian] =   request.TaskDescriptionEt ??  String.Empty,
            },
            Priority = request.Priority,
            ExperimentId = request.ExperimentId,
            Status = request.Status,
            TaskTypeId =  request.TaskTypeId,
            AssignedUserId = request.AssignedUserId,   
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ExperimentTask entity, SaveExperimentTaskRequest request)
    {
        entity.TaskName.SetTranslation(request.TaskDescriptionEn ?? String.Empty, Cultures.English);
        entity.TaskName.SetTranslation(request.TaskDescriptionEt ?? String.Empty, Cultures.Estonian);

        entity.TaskDescription ??= new LangStr();
        entity.TaskDescription.SetTranslation(request.TaskDescriptionEn ?? String.Empty, Cultures.English);
        entity.TaskDescription.SetTranslation(request.TaskDescriptionEt ?? String.Empty, Cultures.Estonian);
        
        entity.Priority = request.Priority;
        entity.ExperimentId = request.ExperimentId;
        entity.Status = request.Status;
        entity.TaskTypeId = request.TaskTypeId;
        entity.AssignedUserId = request.AssignedUserId;
    }
    public static SaveExperimentTaskRequest ToUpdateRequest(ExperimentTask request)
    {
        return new SaveExperimentTaskRequest
        {
            TaskNameEn = request.TaskName.Translate("en") ?? String.Empty,
            TaskNameEt = request.TaskName.Translate("et") ??  String.Empty,
            TaskDescriptionEn = request.TaskDescription?.Translate("en"),
            TaskDescriptionEt = request.TaskDescription?.Translate("et"),
            Priority = request.Priority,
            ExperimentId = request.ExperimentId,
            Status = request.Status,
            TaskTypeId = request.TaskTypeId,
            AssignedUserId = request.AssignedUserId
        };
    }
}