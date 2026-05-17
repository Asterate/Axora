using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;
using App.Shared.Contracts;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Project.Application.Mappers;

public static class ScheduleMapper
{
    // Entity → List Response
    public static ScheduleListResponse ToListResponse(Schedule entity, 
        LookupItem? lab, 
        LookupItem? equipment)
        => new ()
        {
            Id = entity.Id,
            ScheduleName =  entity.ScheduleName.Translate(),
            Status = entity.Status,
            ColorCode =  entity.ColorCode?.Translate(),
            CreatedAt = entity.CreatedAt,
            ExperimentTaskName = entity.ExperimentTask.TaskName.Translate(),
            ScheduleStartTime = entity.ScheduleStartTime,
            ScheduleEndTime =  entity.ScheduleEndTime,
            LabName = lab?.Name,
            EquipmentName =  equipment?.Name,
        };

    // Entity → Full Response
    public static ScheduleResponse ToResponse(Schedule entity, 
        LookupItem? lab, 
        LookupItem? equipment)
        => new ()
        {
            Id = entity.Id,
            ScheduleName =  entity.ScheduleName.Translate(),
            Status = entity.Status,
            ColorCode =  entity.ColorCode?.Translate(),
            CreatedAt = entity.CreatedAt,
            ExperimentTaskName = entity.ExperimentTask.TaskName.Translate(),
            LabName = lab?.Name,
            InstituteUserId = entity.InstituteUserId,
            EquipmentName =  equipment?.Name,
            ScheduleStartTime = entity.ScheduleStartTime,
            ScheduleEndTime =  entity.ScheduleEndTime,
        };

    // Create Request → Entity
    public static Schedule ToEntity(SaveScheduleRequest request)
        => new ()
        {
            ScheduleName =  new LangStr()
            {
                [Cultures.English] =  request.ScheduleNameEn ?? String.Empty,
                [Cultures.Estonian] =  request.ScheduleNameEt ?? String.Empty,
            },
            Status = request.Status,
            ColorCode =  new LangStr()
            {
                [Cultures.English] =  request.ColorCodeEn ?? String.Empty,
                [Cultures.Estonian] =  request.ColorCodeEt ?? String.Empty,
            },
            ExperimentTaskId = request.ExperimentId,
            LabId = request.LabId,
            InstituteUserId = request.InstituteUserId,
            EquipmentId =  request.EquipmentId,
            ScheduleStartTime = request.ScheduleStartTime,
            ScheduleEndTime =  request.ScheduleEndTime,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Schedule entity, SaveScheduleRequest request)
    {
        entity.ScheduleName.SetTranslation(request.ScheduleNameEn ?? "??", Cultures.English);
        entity.ScheduleName.SetTranslation(request.ScheduleNameEt ?? "??", Cultures.Estonian);
        entity.Status = request.Status;
        entity.ColorCode ??= new LangStr();
        entity.ColorCode.SetTranslation(request.ColorCodeEn ?? "??", Cultures.English);
        entity.ColorCode.SetTranslation(request.ColorCodeEt ?? "??", Cultures.Estonian);
        entity.ExperimentTaskId = request.ExperimentId;
        entity.LabId = request.LabId;
        entity.InstituteUserId = request.InstituteUserId;
        entity.EquipmentId = request.EquipmentId;
        entity.ScheduleStartTime = entity.ScheduleStartTime;
        entity.ScheduleEndTime = entity.ScheduleEndTime;
    }
    public static SaveScheduleRequest ToUpdateRequest(Schedule request)
    {
        return new SaveScheduleRequest
        {
            ScheduleNameEn =  request.ScheduleName.Translate(Cultures.English),
            ScheduleNameEt =  request.ScheduleName.Translate(Cultures.Estonian),
            Status = request.Status,
            ColorCodeEn = request.ColorCode?.Translate(Cultures.English),
            ColorCodeEt = request.ColorCode?.Translate(Cultures.Estonian),
            LabId = request.LabId,
            InstituteUserId = request.InstituteUserId,
            EquipmentId =  request.EquipmentId,
            ScheduleStartTime = request.ScheduleStartTime,
            ScheduleEndTime =  request.ScheduleEndTime,
            
        };
    }
}