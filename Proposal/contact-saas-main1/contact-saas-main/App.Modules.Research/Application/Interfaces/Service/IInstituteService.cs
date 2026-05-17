using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IInstituteService
{
    Task<IEnumerable<InstituteListResponse>> GetAllAsync();
    Task<InstituteResponse?> GetByIdAsync(Guid id);
    Task<InstituteResponse> CreateAsync(SaveInstituteRequest request);
    Task UpdateAsync(Guid id, SaveInstituteRequest request);
    Task DeleteAsync(Guid id);
    Task<int> CountAsync();
    Task<IEnumerable<InstituteListResponse>> GetRecentAsync(int take);
    Task<IEnumerable<InstituteResponse>> FindDeletedAsync();
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
    Task<Domain.Institute?> GetEntityByIdAsync(Guid id);
    Task<Domain.Institute> CreateAndReturnAsync(SaveInstituteRequest request);
    Task<Domain.Institute?> GetFirstActiveAsync();
}