using App.Modules.Equipment.Domain;
using App.Shared.Domain;

namespace App.Modules.Lab.Domain;

public class EquipmentCertificationType : BaseEntity
{
        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; }  = default!;
        

        public Guid CertificationTypeId { get; set; }
        public CertificationType CertificationType { get; set; }  = default!;
}