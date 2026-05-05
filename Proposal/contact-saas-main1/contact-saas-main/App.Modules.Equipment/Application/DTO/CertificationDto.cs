namespace Modules.Equipment.Application.DTO;

public class CertificationDto
{
    public Guid Id { get; set; }
    public string CertificationName { get; set; } = default!;
    public DateTime HandedOver { get; set; }
    public DateTime? Expired { get; set; }
    public Guid InstituteUserId { get; set; }
    
    // CertificationType data included directly
    public Guid CertificationTypeId { get; set; }
    public string? CertificationTypeName { get; set; }
    public string? Description { get; set; }
}
