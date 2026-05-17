using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Project.Domain;

public class ProjectType : BaseEntity
{
    public LangStr Name { get; set; } = "??";

    public LangStr? Description { get; set; }
    
}