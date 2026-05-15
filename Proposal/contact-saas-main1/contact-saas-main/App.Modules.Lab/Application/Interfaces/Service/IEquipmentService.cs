using App.Modules.Lab.Application.DTO;

namespace App.Modules.Lab.Application.Interfaces;

public interface IEquipmentService
{
    Task<IEnumerable<EquipmentListResponse>> GetAllAsync();
    Task<EquipmentResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateEquipmentRequest request);
    Task UpdateAsync(Guid id, UpdateEquipmentRequest request);
    Task DeleteAsync(Guid id);
}