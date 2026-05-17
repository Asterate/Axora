using App.Modules.Project.Application.DTO;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IResultService
{
    Task<IEnumerable<ResultListResponse>> GetAllAsync();
    Task<ResultResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveResultRequest request);
    Task UpdateAsync(Guid id, SaveResultRequest request);
    Task DeleteAsync(Guid id);
}