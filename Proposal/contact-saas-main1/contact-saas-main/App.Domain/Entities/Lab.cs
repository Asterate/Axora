using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Resources;

namespace App.Domain.Entities;

public class Lab : BaseEntity
{
    [Display(Name = "Name", ResourceType = typeof(Base.Resources.Common))]
    [StringLength(128, MinimumLength = 3)]
    public string LabName { get; set; }  = default!;
    
    [Display(Name = "Address", ResourceType = typeof(Base.Resources.Common))]
    [StringLength(128, MinimumLength = 3)]
    public string LabAddress { get; set; }  = default!;
    
    [Display(Name = "Capacity", ResourceType = typeof(Base.Resources.Common))]
    public int LabCapacity { get; set; }
    
    [Display(Name = "CreatedAt", ResourceType = typeof(Base.Resources.Common))]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Display(Name = "UpdatedAt", ResourceType = typeof(Base.Resources.Common))]
    public DateTime? UpdatedAt { get; set; }
    
    [Display(Name = "DeletedAt", ResourceType = typeof(Base.Resources.Common))]
    public DateTime? DeletedAt { get; set; }
    
    [Display(Name = "IsActive", ResourceType = typeof(Base.Resources.Common))]
    public bool LabIsActive { get; set; } = true;
    
    public Guid LabTypeId { get; set; }
    public LabType LabType { get; set; } = default!;
}