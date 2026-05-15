using App.Modules.Lab.Application.DTO;

namespace App.Modules.Lab.Application.Interfaces;

public interface IEquipmentLabService
{
    Task<IEnumerable<EquipmentLabResponse>> GetAllAsync();
    Task<EquipmentLabResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateEquipmentLabRequest request);
    Task UpdateAsync(Guid id, UpdateEquipmentLabRequest request);
    Task DeleteAsync(Guid id);
}