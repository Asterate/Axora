using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;
using App.Shared.Domain;

namespace App.Modules.Project.Application.Mappers;

public static class ExperimentTaskMapper
{
    // Entity → List Response
    public static ExperimentTaskListResponse ToListResponse(ExperimentTask entity)
        => new ()
        {
            Id = entity.Id,
            Name = entity.GetTaskName("en") ?? "?",
            Description = entity.GetTaskDescription("en"),
            Priority = entity.Priority,
            ExperimentName = entity.GetTaskName("en") ?? "?",
            Status = entity.Status,
            CreatedAt =  entity.CreatedAt,
            UpdatedAt =  entity.UpdatedAt,
        };

    // Entity → Full Response
    public static ExperimentTaskResponse ToResponse(ExperimentTask entity)
        => new ()
        {
            Id = entity.Id,
            NameEn = entity.GetTaskName("en") ?? "?",
            NameEt = entity.GetTaskName("et") ?? "?",
            DescriptionEn = entity.GetTaskDescription("en"),
            DescriptionEt = entity.GetTaskDescription("et"),
            Priority = entity.Priority,
            ExperimentId = entity.ExperimentId,
            Status = entity.Status,
            CreatedAt =  entity.CreatedAt,
            UpdatedAt =  entity.UpdatedAt,
            TaskTypeId =  entity.TaskTypeId,
            AssignedUserId = entity.AssignedUserId,
        };

    // Create Request → Entity
    public static ExperimentTask ToEntity(CreateExperimentTaskRequest request)
        => new ()
        {
            TaskName = request.TaskName ?? "?",
            TaskDescription = request.TaskDescription ?? "?",
            Priority = request.Priority,
            ExperimentId = request.ExperimentId,
            Status = request.Status,
            CreatedAt =  request.CreatedAt,
            UpdatedAt =  request.UpdatedAt,
            TaskTypeId =  request.TaskTypeId,
            AssignedUserId = request.AssignedUserId,   
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ExperimentTask entity, UpdateExperimentTaskRequest request)
    {
        entity.Id = request.Id;
        entity.TaskName = request.TaskName;
        entity.TaskDescription = request.TaskDescription;
        entity.Priority = request.Priority;
        entity.ExperimentId = request.ExperimentId;
        entity.Status = request.Status;
        entity.CreatedAt = request.CreatedAt;
        entity.UpdatedAt = request.UpdatedAt;
        entity.TaskTypeId = request.TaskTypeId;
        entity.AssignedUserId = request.AssignedUserId;
    }
    public static UpdateExperimentTaskRequest ToUpdateRequest(ExperimentTask request)
    {
        return new UpdateExperimentTaskRequest
        {
            Id = request.Id,
            TaskName = request.TaskName ?? "?",
            TaskDescription = request.TaskDescription ?? "?",
            Priority = request.Priority,
            ExperimentId = request.ExperimentId,
            Status = request.Status,
            CreatedAt =  request.CreatedAt,
            UpdatedAt =  request.UpdatedAt,
            TaskTypeId =  request.TaskTypeId,
            AssignedUserId = request.AssignedUserId, 
        };
    }
}