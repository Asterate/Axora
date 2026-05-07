using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain;
using App.Shared.Domain;

namespace App.Domain.Entities;

public class Project : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public LangStr ProjectName { get; set; } = new();
    
    public float? Funding { get; set; }
    
    public LangStr? Requirements { get; set; } 
    
    public string? RequirementsFilePath { get; set; }
    
    public Guid ProjectTypeId {get; set;}
    public ProjectType ProjectType {get; set;}  = default!;
}