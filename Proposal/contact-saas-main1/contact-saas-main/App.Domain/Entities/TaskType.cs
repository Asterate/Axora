using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class TaskType : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string TaskTypeName { get; set; }  = default!;
    [StringLength(128, MinimumLength = 3)]
    public string? TaskTypeDescription { get; set; }
}