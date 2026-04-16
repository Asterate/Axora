using App.Domain;

namespace App.Domain.Entities;

public class EquipmentType : BaseEntity
{
    public LangStr EquipmentTypeName { get; set; } = new();
    public LangStr? EquipmentTypeDescription { get; set; }
}