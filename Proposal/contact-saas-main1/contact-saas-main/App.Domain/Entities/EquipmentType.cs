using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class EquipmentType : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string EquipmentTypeName { get; set; }  = default!;
    [StringLength(128, MinimumLength = 3)]
    public string? EquipmentTypeDescription { get; set; }
}