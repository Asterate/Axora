namespace App.Modules.Lab.Application.DTO;


public class CertificationResponse
{
    public Guid Id { get; set; }
    public string CertificationName { get; set; } = "??";
    public DateTime HandedOver { get; set; }
    public DateTime? Expired { get; set; }
    public Guid InstituteUserId { get; set; }
    public Guid CertificationTypeId { get; set; }
}

public class SaveCertificationRequest
{
    public string CertificationNameEn { get; set; } = "??";
    public string CertificationNameEt { get; set; } = "??";
    public DateTime HandedOver { get; set; }
    public DateTime? Expired { get; set; }
    public Guid InstituteUserId { get; set; }
    public Guid CertificationTypeId { get; set; }
}