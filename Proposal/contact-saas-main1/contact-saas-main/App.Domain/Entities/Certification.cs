
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities;

public class Certification : BaseEntity
{
    public string CertificationName { get; set; }  = default!;
    public DateTime HandedOver { get; set; }
    public DateTime? Expired { get; set; }
    public Guid InstituteUserId { get; set; }
    public InstituteUser InstituteUser { get; set; } = default!;
    public Guid CertificationTypeId { get; set; }
    [ForeignKey(nameof(CertificationTypeId))]
    public CertificationType? CertificationType { get; set; }
}