using App.Shared.Domain;

namespace App.Modules.Equipment.Domain;

public class EquipmentCertificationType : BaseEntity
{
        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; }  = default!;
        

        public Guid CertificationTypeId { get; set; }
        public CertificationType CertificationType { get; set; }  = default!;
}