using App.Shared.Domain;

namespace App.Modules.Lab.Domain;

public class EquipmentCertification : BaseEntity
{
        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; }  = default!;
        

        public Guid CertificationTypeId { get; set; }
        public CertificationType CertificationType { get; set; }  = default!;
}