using App.Modules.Lab.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Interfaces.Service;

public interface IEquipmentTypeService
{
    Task<IEnumerable<EquipmentTypeResponse>> GetAllAsync();
    Task<EquipmentTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveEquipmentTypeRequest request);
    Task UpdateAsync(Guid id, SaveEquipmentTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}