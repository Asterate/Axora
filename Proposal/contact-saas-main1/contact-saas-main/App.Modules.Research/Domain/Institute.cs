using App.Domain.Entities;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Project.Domain;

public class Institute : BaseEntity
{
    public string InstituteName { get; set; } = "{}";

    public string InstituteCountry { get; set; } = default!;

    public string InstituteAddress { get; set; } = "{}";

    public string InstitutePhoneNumber { get; set; } = default!;

    public Boolean Active { get; set; } = true;

    public Guid InstituteTypeId { get; set; }
    public InstituteType InstituteType { get; set; } = default!;
    public ICollection<InstituteProject> InstituteProjects { get; set; } = new List<InstituteProject>();

    public string? GetInstituteName(string? culture = null)
        => InstituteName.GetLocalizedValue(culture);

    public string? GetInstituteAddress(string? culture = null)
        => InstituteAddress.GetLocalizedValue(culture);
}