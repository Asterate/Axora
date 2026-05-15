using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IInstituteTypeService
{
    Task<IEnumerable<InstituteTypeListResponse>> GetAllAsync();
    Task<InstituteTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateInstituteTypeRequest request);
    Task UpdateAsync(Guid id, UpdateInstituteTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}