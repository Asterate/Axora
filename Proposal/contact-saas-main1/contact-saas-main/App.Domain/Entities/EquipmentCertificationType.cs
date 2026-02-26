namespace App.Domain.Entities;

public class EquipmentCertificationType : BaseEntity
{
        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; }  = default!;
        

        public Guid CertificationTypeId { get; set; }
        public required CertificationType CertificationType { get; set; }  = default!;
}