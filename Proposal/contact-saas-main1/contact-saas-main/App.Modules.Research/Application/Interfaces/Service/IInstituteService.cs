using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IInstituteService
{
    Task<IEnumerable<InstituteListResponse>> GetAllAsync();
    Task<InstituteResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateInstituteRequest request);
    Task UpdateAsync(Guid id, UpdateInstituteRequest request);
    Task DeleteAsync(Guid id);
    Task<int> CountAsync();
    Task<IEnumerable<InstituteListResponse>> GetRecentAsync(int take);
    Task<IEnumerable<InstituteListResponse>> FindDeletedAsync();
    Task<List<LookupItem>> GetActivesAsync();
    Task<Domain.Institute?> GetEntityByIdAsync(Guid id);
    Task<Domain.Institute> CreateAndReturnAsync(CreateInstituteRequest request);
    Task<Domain.Institute?> GetFirstActiveAsync();
}