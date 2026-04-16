using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain;
using Base.Resources;

namespace App.Domain.Entities;

public class CertificationType:BaseEntity
{
    [Display(Name = "Name", ResourceType = typeof(Base.Resources.Common))]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; }  = default!;
    
    [Display(Name = "Description", ResourceType = typeof(Base.Resources.Common))]
    [StringLength(512)]
    public string? Description { get; set; }
}