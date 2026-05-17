using App.Shared.Domain;

namespace App.Modules.Project.Domain;

public class DocumentType : BaseEntity
{
    public LangStr Name { get; set; } = "??";

    public LangStr? Description { get; set; }
    
}