using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain;
using Base.Resources;

namespace App.Domain.Entities;

public class LabType : BaseEntity
{
    [Display(Name = "Name", ResourceType = typeof(Base.Resources.Common))]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } = new();
    
    [Display(Name = "Description", ResourceType = typeof(Base.Resources.Common))]
    [Column(TypeName = "jsonb")]
    public LangStr? Description { get; set; }
}