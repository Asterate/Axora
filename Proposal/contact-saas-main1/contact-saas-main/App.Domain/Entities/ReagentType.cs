using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain;

namespace App.Domain.Entities;

public class ReagentType : BaseEntity
{
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } = new();
    [Column(TypeName = "jsonb")]
    public LangStr? Description { get; set; }
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