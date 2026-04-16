using System.ComponentModel.DataAnnotations;
using App.Domain;

namespace App.Domain.Entities;

public class ReagentType : BaseEntity
{
    public LangStr ReagentName { get; set; } = new();
    public LangStr? ReagentDescription { get; set; }
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