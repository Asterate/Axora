using App.Shared.Domain;

namespace App.Modules.Lab.Domain;

public class LabType : BaseEntity
{
    public LangStr Name { get; set; } = "??";

    public LangStr? Description { get; set; }
    
}