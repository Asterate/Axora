using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Entities;

[Index(nameof(ReagentId), nameof(LabId), IsUnique = true)]

public class ReagentLab : BaseEntity
{
    public int Quantity { get; set; }
    [StringLength(10, MinimumLength = 1)]
    public string Unit { get; set; }  = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public Guid LabId { get; set; }
    public Lab Lab { get; set; } = default!;
    public Guid ReagentId { get; set; }
    public Reagent Reagant { get; set; } = default!;
}