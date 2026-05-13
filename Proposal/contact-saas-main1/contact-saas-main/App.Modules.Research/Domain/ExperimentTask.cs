using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using App.Domain;
using App.Shared.Domain;

namespace App.Domain.Entities;

public class ExperimentTask : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    [Column(TypeName = "jsonb")]
    public string TaskName { get; set; } = "{}";
    [StringLength(128, MinimumLength = 3)]
    [Column(TypeName = "jsonb")]
    public string? TaskDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
    public EExperimentTaskStatus Status { get; set; } = EExperimentTaskStatus.Pending;
    public int? Priority { get; set; }

    public Guid ExperimentId { get; set; }
    public Experiment Experiment { get; set; } = default!;
    public Guid TaskTypeId { get; set; }
    public ExperimentTaskType ExperimentTaskType { get; set; } = default!;
    public Guid? AssignedUserId { get; set; }
    public EExperimentTaskPriority PriorityType { get; set; } = EExperimentTaskPriority.Low;

    public string? GetTaskName(string? culture = null)
        => GetFromJson(TaskName, culture);

    public string? GetTaskDescription(string? culture = null)
        => GetFromJson(TaskDescription, culture);

    private static string? GetFromJson(string? json, string? culture)
    {
        if (string.IsNullOrWhiteSpace(json)) return null;
        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;
        var neutral = culture.Split('-')[0];
        try
        {
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            if (dict == null) return null;
            if (dict.TryGetValue(culture, out var val)) return val;
            if (dict.TryGetValue(neutral, out val)) return val;
            if (dict.TryGetValue("en", out val)) return val;
            return dict.Values.FirstOrDefault();
        }
        catch
        {
            return null;
        }
    }
}