namespace App.DTO.v1;

/// <summary>
/// Request DTO for creating a new experiment task
/// </summary>
public class CreateTaskRequest
{
    public string TaskName { get; set; } = default!;
    public string? TaskDescription { get; set; }
    public int? Priority { get; set; }
    public Guid TaskTypeId { get; set; }
    public Guid ExperimentId { get; set; }
    public Guid? AssignedUserId { get; set; }
}

/// <summary>
/// Request DTO for updating an experiment task
/// </summary>
public class UpdateTaskRequest
{
    public string TaskName { get; set; } = default!;
    public string? TaskDescription { get; set; }
    public int? Priority { get; set; }
    public Guid TaskTypeId { get; set; }
    public Guid ExperimentId { get; set; }
    public Guid? AssignedUserId { get; set; }
}

/// <summary>
/// Response DTO for experiment task data
/// </summary>
public class ExperimentTaskResponse
{
    public Guid Id { get; set; }
    public string TaskName { get; set; } = default!;
    public string? TaskDescription { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    /// <summary>
    /// Status: 0=Pending, 1=InProgress, 2=Completed, 3=Cancelled
    /// </summary>
    public int Status { get; set; }
    
    public string? InstituteUserName { get; set; }
    public int? Priority { get; set; }
    public Guid TaskTypeId { get; set; }
    public Guid? AssignedUserId { get; set; }
    public Guid ExperimentId { get; set; }
    public string? TaskTypeName { get; set; }
}