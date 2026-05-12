using App.Domain.Entities;
using App.Shared.Domain;

public class ExperimentTaskListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class ExperimentTaskResponse
{
    public Guid Id { get; set; }
    public string? NameEn { get; set; }
    public string? NameEt { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
    public EExperimentTaskStatus Status { get; set; }
    public int? Priority { get; set; }
    
    public Guid ExperimentId { get; set; }
    public Experiment Experiment { get; set; } = default!;
    public Guid TaskTypeId { get; set; }
    public ExperimentTaskType ExperimentTaskType { get; set; } = default!;
    public Guid? AssignedUserId { get; set; }
    public EExperimentTaskPriority PriorityType { get; set; }
}

public class CreateExperimentTaskRequest
{
    public Guid Id { get; set; }
    public LangStr TaskName { get; set; } = new();
    public LangStr? TaskDescription { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public EExperimentTaskStatus Status { get; set; }
    public int? Priority { get; set; }
    public Guid ExperimentId { get; set; }
    public Guid TaskTypeId { get; set; }
    public Guid? AssignedUserId { get; set; }
}

public class UpdateExperimentTaskRequest
{
    public Guid Id { get; set; }
    public LangStr TaskName { get; set; } = new();
    public LangStr? TaskDescription { get; set; }
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public EExperimentTaskStatus Status { get; set; }
    public int? Priority { get; set; }
    public Guid ExperimentId { get; set; }
    public Guid TaskTypeId { get; set; }
    public Guid? AssignedUserId { get; set; }
    
    public UpdateExperimentTaskRequest(ExperimentTask experimentTask)
    {
        Id = experimentTask.Id;
        TaskName = experimentTask.TaskName;
        TaskDescription = experimentTask.TaskDescription;
        Status = experimentTask.Status;
        Priority = experimentTask.Priority;
        ExperimentId = experimentTask.ExperimentId;
        TaskTypeId = experimentTask.TaskTypeId;
        AssignedUserId = experimentTask.AssignedUserId;
    }
}