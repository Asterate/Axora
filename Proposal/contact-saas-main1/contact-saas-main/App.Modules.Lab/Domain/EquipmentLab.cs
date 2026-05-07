using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Entities;
using App.Modules.Equipment.Domain;
using App.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Equipment.Domain;

[Index(nameof(EquipmentId), nameof(LabId), IsUnique = true)]
public class EquipmentLab : BaseEntity
{
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public Guid LabId { get; set; }
    public Lab Lab { get; set; }  = default!;
    public Guid EquipmentId {get; set;}
}