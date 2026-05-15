using App.Modules.Lab.Application.DTO;

namespace App.Modules.Lab.Application.Interfaces;

public interface IEquipmentCertificationService
{
    Task<IEnumerable<EquipmentCertificationListResponse>> GetAllAsync();
    Task<EquipmentCertificationResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateEquipmentCertificationRequest request);
    Task DeleteAsync(Guid id);
}