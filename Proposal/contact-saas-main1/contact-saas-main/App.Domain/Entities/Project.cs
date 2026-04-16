using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain;
using Base.Resources;

namespace App.Domain.Entities;

public class Project : BaseEntity
{
    [Display(Name = "Name", ResourceType = typeof(Base.Resources.Common))]
    [StringLength(128, MinimumLength = 3)]
    [Column(TypeName = "jsonb")]
    public LangStr ProjectName { get; set; } = new();
    
    [Display(Name = "Funding", ResourceType = typeof(Base.Resources.Common))]
    public float? Funding { get; set; }
    
    [Display(Name = "Requirements", ResourceType = typeof(Base.Resources.Common))]
    [Column(TypeName = "jsonb")]
    public LangStr? Requirements { get; set; } 
    
    [Display(Name = "FilePath", ResourceType = typeof(Base.Resources.Common))]
    public string? RequirementsFilePath { get; set; }
    
    public Guid ProjectTypeId {get; set;}
    public ProjectType ProjectType {get; set;}  = default!;
    public ICollection<Experiment> Experiments { get; set; } = new List<Experiment>();
}