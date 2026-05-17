
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
    public string? LabName { get; set; }
    public string? EquipmentName { get; set; }
}

public class ScheduleResponse
{
    public Guid Id { get; set; }
    public string? ScheduleName { get; set; }
    public EScheduleStatus Status { get; set; }
    public string? ColorCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public string? LabName { get; set; }
    public Guid InstituteUserId { get; set; }
    public string? EquipmentName { get; set; }
    public string? ExperimentTaskName { get; set; }
    public DateTime ScheduleEndTime { get; set; }
    public DateTime ScheduleStartTime { get; set; }
}

public class SaveScheduleRequest
{
    public string? ScheduleNameEn { get; set; }
    public string? ScheduleNameEt { get; set; }
    public EScheduleStatus Status { get; set; }
    public string? ColorCodeEn { get; set; }
    public string? ColorCodeEt { get; set; }
    public Guid LabId { get; set; }
    public Guid InstituteUserId { get; set; }
    public Guid EquipmentId { get; set; }
    public Guid ExperimentId { get; set; }
    public DateTime ScheduleEndTime { get; set; }
    public DateTime ScheduleStartTime { get; set; }
    public bool IsValid() => ScheduleEndTime > ScheduleStartTime;
}