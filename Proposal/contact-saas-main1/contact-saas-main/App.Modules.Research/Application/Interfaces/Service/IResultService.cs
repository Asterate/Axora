using App.Modules.Project.Application.DTO;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IResultService
{
    Task<IEnumerable<ResultListResponse>> GetAllAsync();
    Task<ResultResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateResultRequest request);
    Task UpdateAsync(Guid id, UpdateResultRequest request);
    Task DeleteAsync(Guid id);
}