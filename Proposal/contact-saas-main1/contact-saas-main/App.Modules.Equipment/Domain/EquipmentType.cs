using App.Shared.Domain;

namespace App.Modules.Equipment.Domain;

public class EquipmentType : BaseEntity
{
    public LangStr Name { get; set; } = new();
    
    public LangStr? Description { get; set; }
}