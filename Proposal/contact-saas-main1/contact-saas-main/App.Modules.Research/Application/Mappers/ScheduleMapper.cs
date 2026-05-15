using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;

namespace App.Modules.Project.Application.Mappers;

public static class ScheduleMapper
{
    // Entity → List Response
    public static ScheduleListResponse ToListResponse(Schedule entity)
        => new ()
        {
            Id = entity.Id,
            ScheduleName =  entity.ScheduleName,
            Status = entity.Status,
            ColorCode =  entity.ColorCode,
            CreatedAt = entity.CreatedAt,
            ExperimentTaskName = entity.Experiment.ExperimentName,
            ScheduleStartTime = entity.ScheduleStartTime,
            ScheduleEndTime =  entity.ScheduleEndTime,
        };

    // Entity → Full Response
    public static ScheduleResponse ToResponse(Schedule entity)
        => new ()
        {
            Id = entity.Id,
            ScheduleName =  entity.ScheduleName,
            Status = entity.Status,
            ColorCode =  entity.ColorCode,
            CreatedAt = entity.CreatedAt,
            ExperimentTaskName = entity.Experiment.ExperimentName,
            LabId = entity.LabId,
            InstituteUserId = entity.InstituteUserId,
            EquipmentId =  entity.EquipmentId,
            ScheduleStartTime = entity.ScheduleStartTime,
            ScheduleEndTime =  entity.ScheduleEndTime,
        };

    // Create Request → Entity
    public static Schedule ToEntity(CreateScheduleRequest request)
        => new ()
        {
            ScheduleName =  request.ScheduleName ?? "??",
            Status = request.Status,
            ColorCode =  request.ColorCode,
            CreatedAt = request.CreatedAt,
            ExperimentTaskId = request.ExperimentId,
            LabId = request.LabId,
            InstituteUserId = request.InstituteUserId,
            EquipmentId =  request.EquipmentId,
            ScheduleStartTime = request.ScheduleStartTime,
            ScheduleEndTime =  request.ScheduleEndTime,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Schedule entity, UpdateScheduleRequest request)
    {
        entity.Id = request.Id;
        entity.ScheduleName = request.ScheduleName ?? "??";
        entity.Status = request.Status;
        entity.ColorCode = request.ColorCode;
        entity.CreatedAt = request.CreatedAt;
        entity.ExperimentTaskId = request.ExperimentId;
        entity.LabId = request.LabId;
        entity.InstituteUserId = request.InstituteUserId;
        entity.EquipmentId = request.EquipmentId;
        entity.ScheduleStartTime = entity.ScheduleStartTime;
        entity.ScheduleEndTime = entity.ScheduleEndTime;
    }
    public static UpdateScheduleRequest ToUpdateRequest(Schedule request)
    {
        return new UpdateScheduleRequest
        {
            Id = request.Id,
            ScheduleName =  request.ScheduleName ?? "??",
            Status = request.Status,
            ColorCode =  request.ColorCode,
            CreatedAt = request.CreatedAt,
            LabId = request.LabId,
            InstituteUserId = request.InstituteUserId,
            EquipmentId =  request.EquipmentId,
            ScheduleStartTime = request.ScheduleStartTime,
            ScheduleEndTime =  request.ScheduleEndTime,
            
        };
    }
}