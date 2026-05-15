using App.Domain.Entities;
using App.Modules.Project.Domain;

namespace App.Modules.Project.Application.DTO;

public class ExperimentTaskListResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "?";
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public EExperimentTaskStatus Status { get; set; }
    public int? Priority { get; set; }
    public string ExperimentName { get; set; } = "?";
}

public class ExperimentTaskResponse
{
    public Guid Id { get; set; }
    public string NameEn { get; set; } = "?";
    public string NameEt { get; set; } = "?";
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
    public EExperimentTaskStatus Status { get; set; }
    public int? Priority { get; set; }

    public Guid ExperimentId { get; set; }
    public Domain.Experiment Experiment { get; set; } = default!;
    public Guid TaskTypeId { get; set; }
    public ExperimentTaskType ExperimentTaskType { get; set; } = default!;
    public Guid? AssignedUserId { get; set; }
    public EExperimentTaskPriority PriorityType { get; set; }
}

public class CreateExperimentTaskRequest
{
    public string TaskName { get; set; } = "?";
    public string? TaskDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public EExperimentTaskStatus Status { get; set; }
    public int? Priority { get; set; }
    public Guid ExperimentId { get; set; }
    public Guid TaskTypeId { get; set; }
    public Guid? AssignedUserId { get; set; }
}

public class UpdateExperimentTaskRequest : CreateExperimentTaskRequest
{
    public Guid Id { get; set; }
}
    