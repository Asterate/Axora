using App.Modules.Lab.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Interfaces.Service;

public interface ILabService
{
    Task<IEnumerable<LabResponse>> GetAllAsync();
    Task<LabResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveLabRequest request);
    Task UpdateAsync(Guid id, SaveLabRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
    Task<int> CountAsync();
}