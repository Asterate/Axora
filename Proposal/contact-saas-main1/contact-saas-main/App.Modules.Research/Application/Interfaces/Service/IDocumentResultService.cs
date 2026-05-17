using App.Modules.Project.Application.DTO;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IDocumentResultService
{
    Task<IEnumerable<DocumentResultResponse>> GetAllAsync();
    Task<DocumentResultResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveDocumentResultRequest request);
    Task UpdateAsync(Guid id, SaveDocumentResultRequest request);
    Task DeleteAsync(Guid id);
}