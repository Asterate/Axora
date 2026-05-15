using App.Modules.Lab.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Interfaces;

public interface IEquipmentTypeService
{
    Task<IEnumerable<EquipmentTypeListResponse>> GetAllAsync();
    Task<EquipmentTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateEquipmentTypeRequest request);
    Task UpdateAsync(Guid id, UpdateEquipmentTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}