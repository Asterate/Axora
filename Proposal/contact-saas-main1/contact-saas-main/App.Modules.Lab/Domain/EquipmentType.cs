using App.Shared.Domain;

namespace App.Modules.Lab.Domain;

public class EquipmentType : BaseEntity
{
    public LangStr Name { get; set; } = "??";

    public LangStr? Description { get; set; }
    
}