using App.Modules.Lab.Application.DTO;

namespace App.Modules.Lab.Application.Interfaces.Service;

public interface IEquipmentLabService
{
    Task<IEnumerable<EquipmentLabResponse>> GetAllAsync();
    Task<EquipmentLabResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveEquipmentLabRequest request);
    Task UpdateAsync(Guid id, SaveEquipmentLabRequest request);
    Task DeleteAsync(Guid id);
}