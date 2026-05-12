using App.Shared.Domain;

namespace App.Modules.Equipment.Domain;

public class Certification : BaseEntity
{
    public string CertificationName { get; set; }  = default!;
    public DateTime HandedOver { get; set; }
    public DateTime? Expired { get; set; }
    public Guid InstituteUserId { get; set; }
    public Guid CertificationTypeId { get; set; }
    public CertificationType? CertificationType { get; set; }
}