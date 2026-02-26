using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class ReagentType : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string ReagentName { get; set; }  = default!;
    [StringLength(128, MinimumLength = 3)]
    public string? ReagentDescription { get; set; }
    [StringLength(64)]
    public string? Category { get; set; }
    [StringLength(64)]
    public string? DefaultStorage { get; set; }
    [StringLength(64)]
    public string? HazardLevel { get; set; }
    [StringLength(64)]
    public string? StandardConcentration { get; set; }
    [StringLength(64)]
    public string? MaterialFilePath { get; set; }
    public bool IsHazardous { get; set; } = false;
    [StringLength(64)]
    public string? ColorCode { get; set; } 
}