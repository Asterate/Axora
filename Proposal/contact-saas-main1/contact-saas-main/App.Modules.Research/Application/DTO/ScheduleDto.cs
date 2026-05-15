
using App.Domain.Entities;

namespace App.Modules.Project.Application.DTO;

public class ScheduleListResponse
{
    public Guid Id { get; set; }
    public string? ScheduleName { get; set; }
    public EScheduleStatus Status { get; set; }
    public string? ColorCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ScheduleEndTime { get; set; }
    public DateTime ScheduleStartTime { get; set; }
    public string? ExperimentTaskName { get; set; }
}

public class ScheduleResponse
{
    public Guid Id { get; set; }
    public string? ScheduleName { get; set; }
    public EScheduleStatus Status { get; set; }
    public string? ColorCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid LabId { get; set; }
    public Guid InstituteUserId { get; set; }
    public Guid EquipmentId { get; set; }
    public string? ExperimentTaskName { get; set; }
    public DateTime ScheduleEndTime { get; set; }
    public DateTime ScheduleStartTime { get; set; }
}

public class CreateScheduleRequest
{
    public string? ScheduleName { get; set; }
    public EScheduleStatus Status { get; set; }
    public string? ColorCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid LabId { get; set; }
    public Guid InstituteUserId { get; set; }
    public Guid EquipmentId { get; set; }
    public Guid ExperimentId { get; set; }
    public DateTime ScheduleEndTime { get; set; }
    public DateTime ScheduleStartTime { get; set; }
    public bool IsValid() => ScheduleEndTime > ScheduleStartTime;
}

public class UpdateScheduleRequest :  CreateScheduleRequest
{
    public Guid Id { get; set; }
    
}