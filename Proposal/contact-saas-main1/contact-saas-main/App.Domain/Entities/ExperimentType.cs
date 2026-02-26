using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class ExperimentType : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string ExperimentTypeName { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string? Description { get; set; }
}