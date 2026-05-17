using App.Modules.Lab.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Interfaces.Service;

public interface IEquipmentService
{
    Task<IEnumerable<EquipmentListResponse>> GetAllAsync();
    Task<EquipmentResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveEquipmentRequest request);
    Task UpdateAsync(Guid id, SaveEquipmentRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}