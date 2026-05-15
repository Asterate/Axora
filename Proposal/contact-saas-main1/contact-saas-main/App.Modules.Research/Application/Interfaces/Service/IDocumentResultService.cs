using App.Modules.Project.Application.DTO;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IDocumentResultService
{
    Task<IEnumerable<DocumentResultResponse>> GetAllAsync();
    Task<DocumentResultResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateDocumentResultRequest request);
    Task UpdateAsync(Guid id, UpdateDocumentResultRequest request);
    Task DeleteAsync(Guid id);
}