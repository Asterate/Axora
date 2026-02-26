using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities;

public class Project : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string ProjectName { get; set; } = default!;
    public float? Funding { get; set; }
    public string? Requirements { get; set; } 
    public string? RequirementsFilePath { get; set; }
    
    public Guid PublicTypeId {get; set;}
    public ProjectType ProjectType {get; set;}  = default!;
}