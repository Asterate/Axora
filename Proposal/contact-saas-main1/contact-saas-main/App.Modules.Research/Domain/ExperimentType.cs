using App.Shared.Domain;
namespace App.Modules.Project.Domain;

public class ExperimentType : BaseEntity
{
    public LangStr Name { get; set; } = "??";

    public LangStr? Description { get; set; }
    
}