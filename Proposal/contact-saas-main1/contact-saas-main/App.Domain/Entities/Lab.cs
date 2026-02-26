using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities;

public class Lab : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string LabName { get; set; }  = default!;
    [StringLength(128, MinimumLength = 3)]
    public string LabAddress { get; set; }  = default!;
    public int LabCapacity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool LabIsActive { get; set; } = true;
    
    public Guid LabTypeId { get; set; }
    public LabType LabType { get; set; } = default!;
}