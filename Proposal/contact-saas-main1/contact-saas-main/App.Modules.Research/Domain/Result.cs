using System.ComponentModel.DataAnnotations;
using App.Shared.Domain;

namespace App.Domain.Entities;

public class Result : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string ResultName { get; set; }  = default!;
    [StringLength(128, MinimumLength = 3)]
    public string ResultDescription { get; set; }  = default!;
    public string? MeasurementName { get; set; }
    public string? MeasurementValue { get; set; }
    public string? Unit { get; set; }
    public string? Notes { get; set; }
    public string? FilePath { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public Guid ExperimentId { get; set; }
    public Guid ExperimentTaskId { get; set; }
    public ICollection<DocumentResult> DocumentResults { get; set; } = new List<DocumentResult>();

}