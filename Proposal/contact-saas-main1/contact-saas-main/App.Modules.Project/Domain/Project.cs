using System.ComponentModel.DataAnnotations;
using App.Shared.Domain;

namespace App.Domain.Entities;

public class Project : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public LangStr ProjectName { get; set; } = new();
    
    public float? Funding { get; set; }
    
    public LangStr? Requirements { get; set; } 
    
    public string? RequirementsFilePath { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public Guid ProjectTypeId {get; set;}
    public ProjectType ProjectType {get; set;}  = default!;
}