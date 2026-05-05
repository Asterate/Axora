using App.Shared.Domain;

namespace App.Modules.Equipment.Domain;

public class CertificationType:BaseEntity
{
    public LangStr Name { get; set; }  = default!;
    
    public string? Description { get; set; }
}