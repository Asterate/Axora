using App.Shared.Domain;

namespace App.Modules.Project.Domain;

public class Institute : BaseEntity
{
    public LangStr InstituteName { get; set; } = "??";

    public LangStr InstituteCountry { get; set; } = default!;

    public string InstituteAddress { get; set; } = "??";

    public string InstitutePhoneNumber { get; set; } = default!;

    public Boolean Active { get; set; } = true;

    public Guid InstituteTypeId { get; set; }
    public InstituteType InstituteType { get; set; } = default!;
    public ICollection<InstituteProject> InstituteProjects { get; set; } = new List<InstituteProject>();
    
}